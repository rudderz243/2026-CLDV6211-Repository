using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeleteConstraintExampleG1.Models;

namespace DeleteConstraintExampleG1.Controllers
{
	public class ModulesController : Controller
	{
		private readonly StudentG1Context _context;

		public ModulesController(StudentG1Context context)
		{
			_context = context;
		}

		// GET: Modules
		public async Task<IActionResult> Index()
		{
			return View(await _context.Modules.ToListAsync());
		}

		// GET: Modules/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var @module = await _context.Modules
				.FirstOrDefaultAsync(m => m.ModuleId == id);
			if (@module == null)
			{
				return NotFound();
			}

			return View(@module);
		}

		// GET: Modules/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Modules/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ModuleId,ModuleCode,ModuleName,ModuleCredits")] Module @module)
		{
			if (ModelState.IsValid)
			{
				_context.Add(@module);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(@module);
		}

		// GET: Modules/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var @module = await _context.Modules.FindAsync(id);
			if (@module == null)
			{
				return NotFound();
			}
			return View(@module);
		}

		// POST: Modules/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ModuleId,ModuleCode,ModuleName,ModuleCredits")] Module @module)
		{
			if (id != @module.ModuleId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(@module);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ModuleExists(@module.ModuleId))
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
			return View(@module);
		}

		// GET: Modules/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			// using LINQ, check the middle table to find any records corresponding to the module we are deleting
			bool moduleHasStudents = await _context.ModuleStudents.AnyAsync(ms => ms.ModuleId == id);
			// if the student still has modules,
			if (moduleHasStudents)
			{
				// create error
				ModelState.AddModelError("ModuleId", "This module still has students assigned.");
				// return error
				return ValidationProblem(ModelState);
			}


			var @module = await _context.Modules
				.FirstOrDefaultAsync(m => m.ModuleId == id);
			if (@module == null)
			{
				return NotFound();
			}

			return View(@module);
		}

		// POST: Modules/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var @module = await _context.Modules.FindAsync(id);
			if (@module != null)
			{
				_context.Modules.Remove(@module);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ModuleExists(int id)
		{
			return _context.Modules.Any(e => e.ModuleId == id);
		}
	}
}
