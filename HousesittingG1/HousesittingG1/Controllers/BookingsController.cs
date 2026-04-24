using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HousesittingG1.Models;
using System.Transactions;

namespace HousesittingG1.Controllers
{
	public class BookingsController : Controller
	{
		private readonly HousesittingContext _context;

		public BookingsController(HousesittingContext context)
		{
			_context = context;
		}

		// GET: Bookings
		//public async Task<IActionResult> Index()
		//{
		//	var housesittingContext = _context.Bookings.Include(b => b.Cust);
		//	return View(await housesittingContext.ToListAsync());
		//}

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
				// we are setting up some new temporary settings for communicating with the database.
				// by default, we run commands at the same time, but in this case, we want to run them 1 by 1
				var transactionOptions = new TransactionOptions
				{
					IsolationLevel = IsolationLevel.Serializable
				};

				// we set up a scope (connection), in order to make use of the temporary settings we defined
				// we mark our settings as required, and allow this connection to run alongside the main one
				using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions,
				TransactionScopeAsyncFlowOption.Enabled))
				{ // this linq statement checks whether the new booking falls within ANY existing booking
					bool isDoubleBooked = await _context.Bookings.AnyAsync(b =>
					booking.StartDate < b.EndDate && booking.EndDate > b.StartDate);
				
					if (isDoubleBooked) { // so if a double booking is going to occur, we return an error
						ModelState.AddModelError("", "Double booking detected, please check dates");
						// these existing lines send the user back to the create page, with the error message
						ViewData["CustId"] = new SelectList(_context.Customers, "CustId", "FullName", booking.CustId);
						return View(booking);
					} else { // else if the dates are fine
						// run the existing code to add a new booking
						_context.Add(booking);
						await _context.SaveChangesAsync();
						// close the temporary connection we created
						scope.Complete();
					}   
				}
				// existing code
				return RedirectToAction(nameof(Index));
			}
			// existing code
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

		private bool BookingExists(int id)
		{
			return _context.Bookings.Any(e => e.BookingId == id);
		}

		public async Task<IActionResult> Index (int? searchCustId, DateTime? searchStartDate, DateTime? searchEndDate)
		{
			// first: we pass through a list of all the customers to the View, in order to populate the DropDownList
			// we get the list of customers from the DB, then do key:value pairs with the id and name
			ViewData["CustList"] = new SelectList(_context.Customers, "CustId", "FullName", searchCustId);
			// second: we create the default values for the start and end date search boxes
			ViewData["CurrentStartDate"] = searchStartDate?.ToString("yyyy-MM-dd");
			ViewData["CurrentEndDate"] = searchEndDate?.ToString("yyyy-MM-dd");

			// third: we determine what the customer would like to filter by, by checking which fields were modified,
			// and which were left blank
			bool hasSearched = searchCustId.HasValue || searchStartDate.HasValue || searchEndDate.HasValue;

			// fourth: we handle what to do when nothing is selected
			// we do this by getting a list of all bookings from the database
			var bookings = _context.Bookings.Include(b => b.Cust).AsQueryable();

			if (!hasSearched) {
				// and returning the full list
				return View(await bookings.ToListAsync());
			}

			// fifth: we filter the list appopriately based on the search parameters
			if (searchCustId.HasValue) {
				// if they filtered by customer, we use LINQ to extract only that customers bookings
				bookings = bookings.Where(b => b.CustId == searchCustId.Value);
			}

			if (searchStartDate.HasValue) {
				// if they selected a specific start date, we filter for all bookings equal to, or after that date
				bookings = bookings.Where(b => b.StartDate >= searchStartDate.Value);
			}

			if (searchEndDate.HasValue) {
				// if they selected a specific end date, we filter for all bookings equal to, or before that date
				bookings = bookings.Where(b => b.EndDate <= searchEndDate.Value);
			}
			// sixth: once filtering is complete, we return the new dataset
			return View(await bookings.ToListAsync());
		}
	}
}
