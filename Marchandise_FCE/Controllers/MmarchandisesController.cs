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
    public class MmarchandisesController : Controller
    {
        private readonly AnnuaireContext _context;

        public MmarchandisesController(AnnuaireContext context)
        {
            _context = context;
        }

        // GET: Mmarchandises
        public IActionResult Index()
        {
            var annuaireContext = _context.Marchandises.Include(m => m.Categorie);
            return View(annuaireContext.ToList());
        }

       

        // GET: Mmarchandises/Create
        public IActionResult Create()
        {
            ViewData["codcat"] = new SelectList(_context.Categories, "codcat", "codcat");
            return View();
        }

        // POST: Mmarchandises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("refmarch,nature,codcat")] Mmarchandise mmarchandise)
        {
            if (_context.Marchandises.Any(ma => ma.refmarch.Equals(mmarchandise.refmarch)) && _context.Marchandises.Any(ma => ma.nature.Equals(mmarchandise.nature)))
            {
                ViewBag.Erreur = "Ce marchandise est dèjà dans notre liste";
                ViewData["codcat"] = new SelectList(_context.Categories, "codcat", "codcat", mmarchandise.codcat);
                return View();
            }
            _context.Add(mmarchandise);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Mmarchandises/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mmarchandise = await _context.Marchandises.FindAsync(id);
            if (mmarchandise == null)
            {
                return NotFound();
            }
            ViewData["codcat"] = new SelectList(_context.Categories, "codcat", "codcat", mmarchandise.codcat);
            return View(mmarchandise);
        }

        // POST: Mmarchandises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("refmarch,nature,codcat")] Mmarchandise mmarchandise)
        {
            if (id != mmarchandise.refmarch)
            {
                return NotFound();
            }

            if (_context.Marchandises.Any(ma => ma.refmarch.Equals(id)))
            {
                try
                {
                    _context.Update(mmarchandise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MmarchandiseExists(mmarchandise.refmarch))
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
            ViewData["codcat"] = new SelectList(_context.Categories, "codcat", "codcat", mmarchandise.codcat);
            return View(mmarchandise);
        }

        // GET: Mmarchandises/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mmarchandise = await _context.Marchandises
                .Include(m => m.Categorie)
                .FirstOrDefaultAsync(m => m.refmarch == id);
            if (mmarchandise == null)
            {
                return NotFound();
            }

            return View(mmarchandise);
        }

        // POST: Mmarchandises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mmarchandise = await _context.Marchandises.FindAsync(id);
            if (mmarchandise != null)
            {
                _context.Marchandises.Remove(mmarchandise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MmarchandiseExists(string id)
        {
            return _context.Marchandises.Any(e => e.refmarch == id);
        }
    }
}
