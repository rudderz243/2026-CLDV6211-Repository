using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BirdWatcherG2.Models;
using BirdWatcherG2.Services;

namespace BirdWatcherG2.Controllers
{
    public class SpottingsController : Controller
    {
        private readonly BirdWatcherContext _context;
        // call in our blob service
        // (the underscore is the naming convention for a SINGLETON - a single instance
        // of a class that handles all interaction with that class)
        private readonly BlobService _blob;

        // add our blobservice call to the constructor for our controller
        // this is to ensure the singleton is created when the class is called
        public SpottingsController(BirdWatcherContext context, BlobService blob)
        {
            _context = context;
            // blob
            _blob = blob;
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
        [ValidateAntiForgeryToken]
        //                                                                       REMOVE IMAGEURL FROM HERE               ADD FILE HERE
        public async Task<IActionResult> Create([Bind("SpottingId,BirdSeenDescription,TimeSeen")] Spotting spotting, IFormFile imageFile)
        {
            // if the file is PRESENT and NOT EMPTY
            if (imageFile != null && imageFile.Length > 0) {
                // wait for the file to upload to the blob, then get the URL to where it lives
                string uploadedUrl = await _blob.UploadImageAsync(imageFile);

                // UPDATE our model to have the imageURL
                spotting.ImageUrl = uploadedUrl;
            }
            // business as usual
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
        public async Task<IActionResult> Edit(int id, [Bind("SpottingId,BirdSeenDescription,TimeSeen,ImageUrl")] Spotting spotting)
        {
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
