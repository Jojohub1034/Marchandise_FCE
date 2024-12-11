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
    public class MgaresController : Controller
    {
        private readonly AnnuaireContext _context;

        public MgaresController(AnnuaireContext context)
        {
            _context = context;
        }

        // GET: Mgares
        public IActionResult Index()
        {
            var annuaireContext = _context.Gares.Include(m => m.Zones);
            return View(annuaireContext.ToList());
        }

       

        // GET: Mgares/Create
        public IActionResult Create()
        {
            ViewData["codezone"] = new SelectList(_context.Zones, "codezone", "codezone");
            return View();
        }

        // POST: Mgares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("codegare,nomgare,abrevnom,kilometrage,codezone")] Mgare mgare)
        {
            if (_context.Gares.Any(m => m.codegare.Equals(mgare.codegare)))
            {
                ViewBag.Erreur = "Ce gare est déjà là ";
                ViewData["codezone"] = new SelectList(_context.Zones, "codezone", "codezone", mgare.codezone);
                return View();
            }
            _context.Add(mgare);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Mgares/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mgare = await _context.Gares.FindAsync(id);
            if (mgare == null)
            {
                return NotFound();
            }
            ViewData["codezone"] = new SelectList(_context.Zones, "codezone", "codezone", mgare.codezone);
            return View(mgare);
        }

        // POST: Mgares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("codegare,nomgare,abrevnom,kilometrage,codezone")] Mgare mgare)
        {
            if (id != mgare.codegare)
            {
                return NotFound();
            }

            if (_context.Gares.Any(m => m.codegare.Equals(id)))
            {
                try
                {
                    _context.Update(mgare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MgareExists(mgare.codegare))
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
            ViewData["codezone"] = new SelectList(_context.Zones, "codezone", "codezone", mgare.codezone);
            return View(mgare);
        }

        // GET: Mgares/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mgare = await _context.Gares
                .Include(m => m.Zones)
                .FirstOrDefaultAsync(m => m.codegare == id);
            if (mgare == null)
            {
                return NotFound();
            }

            return View(mgare);
        }

        // POST: Mgares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mgare = await _context.Gares.FindAsync(id);
            if (mgare != null)
            {
                _context.Gares.Remove(mgare);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MgareExists(string id)
        {
            return _context.Gares.Any(e => e.codegare == id);
        }
    }
}
