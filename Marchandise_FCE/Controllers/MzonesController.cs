using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Marchandise_FCE.Models;

namespace Marchandise_FCE.Controllers
{
    public class MzonesController : Controller
    {
        private readonly AnnuaireContext _context;

        public MzonesController(AnnuaireContext context)
        {
            _context = context;
        }

        // GET: Mzones
        public IActionResult Index()
        {
            return View(_context.Zones.ToList());
        }

       

        // GET: Mzones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mzones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("codezone,nomzone,descriptzone")] Mzone mzone)
        {
            if (_context.Zones.Any(z => z.codezone.Equals(mzone.codezone)))
            {
                ViewBag.Erreur = "ce zone existe déjà";
                return View(mzone);
            }
            _context.Add(mzone);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Mzones/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mzone = await _context.Zones.FindAsync(id);
            if (mzone == null)
            {
                return NotFound();
            }
            return View(mzone);
        }

        // POST: Mzones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("codezone,nomzone,descriptzone")] Mzone mzone)
        {
            if (id != mzone.codezone)
            {
                return NotFound();
            }

            if (_context.Zones.Any(z => z.codezone.Equals(id)))
            {
                try
                {
                    _context.Update(mzone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MzoneExists(mzone.codezone))
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
            return View(mzone);
        }

        // GET: Mzones/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mzone = await _context.Zones
                .FirstOrDefaultAsync(m => m.codezone == id);
            if (mzone == null)
            {
                return NotFound();
            }

            return View(mzone);
        }

        // POST: Mzones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mzone = await _context.Zones.FindAsync(id);
            if (mzone != null)
            {
                _context.Zones.Remove(mzone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MzoneExists(string id)
        {
            return _context.Zones.Any(e => e.codezone == id);
        }
    }
}
