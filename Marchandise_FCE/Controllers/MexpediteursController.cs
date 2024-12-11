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
    public class MexpediteursController : Controller 
    {
        private readonly AnnuaireContext _context;

        public MexpediteursController(AnnuaireContext context)
        {
            _context = context;
        }

        // GET: Mexpediteurs
        public IActionResult Index()
        {
            return View(_context.Expediteurs.ToList());
        }

       

        // GET: Mexpediteurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mexpediteurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Mexpediteur mexpediteur)
        {
            if (_context.Expediteurs.Any(m => m.idexp.Equals(mexpediteur.idexp)))
            {
                ViewBag.Erreur = "Ce identifiant existe déjà";
                return View();
            }
            _context.Add(mexpediteur);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Mexpediteurs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mexpediteur = await _context.Expediteurs.FindAsync(id);
            if (mexpediteur == null)
            {
                return NotFound();
            }
            return View(mexpediteur);
        }

        // POST: Mexpediteurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("idexp,nomexp,cin,tel")] Mexpediteur mexpediteur)
        {
            if (id != mexpediteur.idexp)
            {
                return NotFound();
            }

            if (_context.Expediteurs.Any(e => e.idexp.Equals(id)))
            {
                try
                {
                    _context.Update(mexpediteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MexpediteurExists(mexpediteur.idexp))
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
            return View(mexpediteur);
        }

        // GET: Mexpediteurs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mexpediteur = await _context.Expediteurs
                .FirstOrDefaultAsync(m => m.idexp == id);
            if (mexpediteur == null)
            {
                return NotFound();
            }

            return View(mexpediteur);
        }

        // POST: Mexpediteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mexpediteur = await _context.Expediteurs.FindAsync(id);
            if (mexpediteur != null)
            {
                _context.Expediteurs.Remove(mexpediteur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MexpediteurExists(string id)
        {
            return _context.Expediteurs.Any(e => e.idexp == id);
        }
    }
}
