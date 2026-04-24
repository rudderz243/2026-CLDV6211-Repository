using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BirdWatcherG1.Models;
using BirdWatcherG1.Services;

namespace BirdWatcherG1.Controllers
{
    public class SpottingsController : Controller
    {
        // these variables are what we call SINGLETONS
        // a singleton is a SINGLE instance of a class, that handles all interaction with that class
        // so _context handles everything to do with the database, _blobService the same with blobs
        // the naming convention is underscore than the name
        private readonly BirdWatcherContext _context;
        private readonly BlobService _blobService;

                                                            // add the blobService to the controller
        public SpottingsController(BirdWatcherContext context, BlobService blobService)
        {
            _context = context;
            // initialize it as well
            _blobService = blobService;
        }

        // GET: Spottings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spottings.ToListAsync());
        }

        // GET: Spottings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spotting = await _context.Spottings
                .FirstOrDefaultAsync(m => m.SpottingId == id);
            if (spotting == null)
            {
                return NotFound();
            }

            return View(spotting);
        }

        // GET: Spottings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spottings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]                                                  // remove ImageURL here            add imageFile here
        public async Task<IActionResult> Create([Bind("SpottingId,SpottingDescription,TimeSeen")] Spotting spotting, IFormFile imageFile)
        {
            // if the file DOES EXIST and is NOT BLANK
            if (imageFile != null && imageFile.Length > 0) {
                // upload the file to the blob storage, and store the returned url in a variable
                string uploadedUrl = await _blobService.UploadImageAsync(imageFile);
                spotting.ImageUrl = uploadedUrl;
            }

            if (ModelState.IsValid)
            {
                _context.Add(spotting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spotting);
        }

        // GET: Spottings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spotting = await _context.Spottings.FindAsync(id);
            if (spotting == null)
            {
                return NotFound();
            }
            return View(spotting);
        }

        // POST: Spottings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpottingId,SpottingDescription,TimeSeen")] Spotting spotting, IFormFile imageFile)
        {
			if (imageFile != null && imageFile.Length > 0)
			{
				// upload the file to the blob storage, and store the returned url in a variable
				string uploadedUrl = await _blobService.UploadImageAsync(imageFile);
				spotting.ImageUrl = uploadedUrl;
			}

			if (id != spotting.SpottingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spotting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpottingExists(spotting.SpottingId))
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
            return View(spotting);
        }

        // GET: Spottings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spotting = await _context.Spottings
                .FirstOrDefaultAsync(m => m.SpottingId == id);
            if (spotting == null)
            {
                return NotFound();
            }

            return View(spotting);
        }

        // POST: Spottings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spotting = await _context.Spottings.FindAsync(id);
            if (spotting != null)
            {
                _context.Spottings.Remove(spotting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpottingExists(int id)
        {
            return _context.Spottings.Any(e => e.SpottingId == id);
        }
    }
}
