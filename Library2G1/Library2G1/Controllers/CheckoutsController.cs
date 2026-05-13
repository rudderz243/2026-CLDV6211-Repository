using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library2G1.Models;
using System.Transactions;

namespace Library2G1.Controllers
{
    public class CheckoutsController : Controller
    {
        private readonly Library2G1Context _context;

        public CheckoutsController(Library2G1Context context)
        {
            _context = context;
        }

        // GET: Checkouts
        public async Task<IActionResult> Index(int? searchCustId, int? searchBookId)
        {
            // create the lists of customers and books for our seaching dropdown lists
            ViewData["CustList"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", searchCustId);
            ViewData["BookList"] = new SelectList(_context.Books, "BookId", "BookTitle", searchBookId);
            // now that we have fed the data into the dropdown lists, we need to check if the user HAS searched
            bool shouldSearch = searchBookId.HasValue || searchCustId.HasValue;
            // if the user did not search, return the default values (ALL loans)
            if (!shouldSearch)
            {
                // (existing code)
                var library2G1Context = _context.Checkouts.Include(c => c.Book).Include(c => c.Customer);
                return View(await library2G1Context.ToListAsync());
            }
            // otherwise, search based on selections
            // to do that, we first need to create a list that we will manipulate
            var filterList = _context.Checkouts.Include(c => c.Book).Include(c => c.Customer).AsQueryable();
            // then we run through each filter
            if (searchCustId.HasValue) {
                filterList = filterList.Where(c => c.CustomerId == searchCustId.Value);
            }
			if (searchBookId.HasValue)
			{
				filterList = filterList.Where(c => c.BookId == searchBookId.Value);
			}
            // return the list
            return View(await filterList.ToListAsync());
		}

        // GET: Checkouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CheckoutId == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        // GET: Checkouts/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookTitle");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            return View();
        }

        // POST: Checkouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckoutId,BookId,CustomerId,StartDate,EndDate")] Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                // set the options to talk to the database
                var transOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.Serializable
                };
                // apply those options
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transOptions,
                TransactionScopeAsyncFlowOption.Enabled))
                {
                    // check for double loans
                    bool doubleLoaned = await _context.Checkouts.AnyAsync(c => checkout.StartDate <= c.EndDate &&
                    checkout.EndDate > c.StartDate);
                    // if loaned out already, error
                    if (doubleLoaned) 
                    {
                        TempData["ErrorMessage"] = "A book cannot be loaned out to two people at the same time.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                // else, continue like nothing happened (original code)
                _context.Add(checkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookTitle", checkout.BookId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", checkout.CustomerId);
            return View(checkout);
        }

        // GET: Checkouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkouts.FindAsync(id);
            if (checkout == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookTitle", checkout.BookId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", checkout.CustomerId);
            return View(checkout);
        }

        // POST: Checkouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CheckoutId,BookId,CustomerId,StartDate,EndDate")] Checkout checkout)
        {
            if (id != checkout.CheckoutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckoutExists(checkout.CheckoutId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookTitle", checkout.BookId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", checkout.CustomerId);
            return View(checkout);
        }

        // GET: Checkouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkout = await _context.Checkouts
                .Include(c => c.Book)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CheckoutId == id);
            if (checkout == null)
            {
                return NotFound();
            }

            return View(checkout);
        }

        // POST: Checkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkout = await _context.Checkouts.FindAsync(id);
            if (checkout != null)
            {
                _context.Checkouts.Remove(checkout);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkouts.Any(e => e.CheckoutId == id);
        }
    }
}
