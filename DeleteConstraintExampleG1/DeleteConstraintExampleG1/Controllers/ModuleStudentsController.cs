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
    public class ModuleStudentsController : Controller
    {
        private readonly StudentG1Context _context;

        public ModuleStudentsController(StudentG1Context context)
        {
            _context = context;
        }

        // GET: ModuleStudents
        public async Task<IActionResult> Index()
        {
            var studentG1Context = _context.ModuleStudents.Include(m => m.Module).Include(m => m.Student);
            return View(await studentG1Context.ToListAsync());
        }

        // GET: ModuleStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleStudent = await _context.ModuleStudents
                .Include(m => m.Module)
                .Include(m => m.Student)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (moduleStudent == null)
            {
                return NotFound();
            }

            return View(moduleStudent);
        }

        // GET: ModuleStudents/Create
        public IActionResult Create()
        {
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName");
            return View();
        }

        // POST: ModuleStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentId,ModuleId,StudentId")] ModuleStudent moduleStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moduleStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode", moduleStudent.ModuleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", moduleStudent.StudentId);
            return View(moduleStudent);
        }

        // GET: ModuleStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleStudent = await _context.ModuleStudents.FindAsync(id);
            if (moduleStudent == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode", moduleStudent.ModuleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", moduleStudent.StudentId);
            return View(moduleStudent);
        }

        // POST: ModuleStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentId,ModuleId,StudentId")] ModuleStudent moduleStudent)
        {
            if (id != moduleStudent.AssignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleStudentExists(moduleStudent.AssignmentId))
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
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode", moduleStudent.ModuleId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", moduleStudent.StudentId);
            return View(moduleStudent);
        }

        // GET: ModuleStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moduleStudent = await _context.ModuleStudents
                .Include(m => m.Module)
                .Include(m => m.Student)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (moduleStudent == null)
            {
                return NotFound();
            }

            return View(moduleStudent);
        }

        // POST: ModuleStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moduleStudent = await _context.ModuleStudents.FindAsync(id);
            if (moduleStudent != null)
            {
                _context.ModuleStudents.Remove(moduleStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleStudentExists(int id)
        {
            return _context.ModuleStudents.Any(e => e.AssignmentId == id);
        }
    }
}
