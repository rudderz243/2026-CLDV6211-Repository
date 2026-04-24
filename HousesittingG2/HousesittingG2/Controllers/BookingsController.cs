using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HousesittingG2.Models;
using System.Transactions;

namespace HousesittingG2.Controllers
{
	public class BookingsController : Controller
	{
		private readonly HousesittingContext _context;

		public BookingsController(HousesittingContext context)
		{
			_context = context;
		}

		// GET: Bookings
		public async Task<IActionResult> Index()
		{
			var housesittingContext = _context.Bookings.Include(b => b.Cust);
			return View(await housesittingContext.ToListAsync());
		}

		// GET: Bookings/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var booking = await _context.Bookings
				.Include(b => b.Cust)
				.FirstOrDefaultAsync(m => m.BookingId == id);
			if (booking == null)
			{
				return NotFound();
			}

			return View(booking);
		}

		// GET: Bookings/Create
		public IActionResult Create()
		{
			ViewData["CustId"] = new SelectList(_context.Customers, "CustId", "FullName");
			return View();
		}

		// POST: Bookings/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("BookingId,CustId,StartDate,EndDate")] Booking booking)
		{
			if (ModelState.IsValid)
			{
				// telling the database to handle our instructions in a 1 by 1 order, rather than all at once
				var transactionOptions = new TransactionOptions
				{
					IsolationLevel = IsolationLevel.Serializable
				};

				// using our new options, create a connection to the database (called scope)
				using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, 
				TransactionScopeAsyncFlowOption.Enabled))
				{   // using linq, perform checks against the new booking to ensure it doesn't conflict with any others
					bool isDoubleBooked = await _context.Bookings.AnyAsync(b =>
					booking.StartDate < b.EndDate && booking.EndDate > b.StartDate);

					if (isDoubleBooked) { // if a double booking is detected, only display an error and return it
						ModelState.AddModelError(string.Empty, "Double Bookings not allowed!");
						ViewData["CustId"] = new SelectList(_context.Customers, "CustId", "FullName", booking.CustId);
						return View(booking);
					} else { // else, add the booking to the database like normal
					// existing code goes here
						_context.Add(booking);
						await _context.SaveChangesAsync();
						scope.Complete();
					}
				}
				return RedirectToAction(nameof(Index));
			} // existing code remains the same here 
			ViewData["CustId"] = new SelectList(_context.Customers, "CustId", "FullName", booking.CustId);
			return View(booking);
		}

		// GET: Bookings/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var booking = await _context.Bookings.FindAsync(id);
			if (booking == null)
			{
				return NotFound();
			}
			ViewData["CustId"] = new SelectList(_context.Customers, "CustId", "FullName", booking.CustId);
			return View(booking);
		}

		// POST: Bookings/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("BookingId,CustId,StartDate,EndDate")] Booking booking)
		{
			if (id != booking.BookingId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(booking);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BookingExists(booking.BookingId))
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
			ViewData["CustId"] = new SelectList(_context.Customers, "CustId", "FullName", booking.CustId);
			return View(booking);
		}

		// GET: Bookings/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var booking = await _context.Bookings
				.Include(b => b.Cust)
				.FirstOrDefaultAsync(m => m.BookingId == id);
			if (booking == null)
			{
				return NotFound();
			}

			return View(booking);
		}

		// POST: Bookings/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var booking = await _context.Bookings.FindAsync(id);
			if (booking != null)
			{
				_context.Bookings.Remove(booking);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		// a ? next to a data type means that the value of the variable can be null (it is nullable)
		public async Task<IActionResult> Search (int? searchCustId, DateTime? searchStartDate, DateTime? searchEndDate)
		{
			// firstly, we create a list of customers, to pass to our dropdown list on the Search page
			ViewData["CustList"] = new SelectList(_context.Customers, "CustId", "FullName", searchCustId);
			// then we format the dates to be the correct format
			ViewData["CurrentStartDate"] = searchStartDate?.ToString("yyyy-MM-dd");
			ViewData["CurrentEndDate"] = searchEndDate?.ToString("yyyy-MM-dd");

			// next, we need to check if the user has entered any search criteria
			// we do this by checking if the search variables have any values assigned to them (if they're not empty)
			bool hasSearched = searchCustId.HasValue || searchStartDate.HasValue || searchEndDate.HasValue;

			// if they have not entered any search queries (i.e., they just click search without changing things), 
			// we just return an empty list of bookings (no data), while we wait for them to filter.
			if (!hasSearched) {
				return View(new List<Booking>());
			}

			// first step of returning the required data - get all the data from the database
			var bookings = _context.Bookings.Include(b => b.Cust).AsQueryable();

			// first check, did they select a customer from the dropdown
			if (searchCustId.HasValue) {
				// if they did, run a LINQ query on the dataset to filter for that specific customer ID
				bookings = bookings.Where(b  => b.CustId == searchCustId.Value);
			}
			// next, startDate
			if (searchStartDate.HasValue) {
				// if they entered a startDate, find all booking that are equal to, or after, the specified start date
				bookings = bookings.Where(b => b.StartDate >=  searchStartDate.Value);
			}
			// finally, endDate
			if (searchEndDate.HasValue) {
				// find all bookings that are equal to, or before the specified end date.
				bookings = bookings.Where(b => b.EndDate <= searchEndDate.Value);
			}
			// then return the filtered list
			return View(await bookings.ToListAsync());

		}

		private bool BookingExists(int id)
		{
			return _context.Bookings.Any(e => e.BookingId == id);
		}
	}
}
