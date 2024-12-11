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
    public class MtarifsController : Controller
    {
        private readonly AnnuaireContext _context;

        public MtarifsController(AnnuaireContext context)
        {
            _context = context;
        }

        // GET: Mtarifs
        public async Task<IActionResult> Index()
        {
            var annuaireContext = _context.Tarifs.Include(m => m.Categorie).Include(m => m.Zone);
            return View(await annuaireContext.ToListAsync());
        }

        // GET: Mtarifs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mtarif = await _context.Tarifs
                .Include(m => m.Categorie)
                .Include(m => m.Zone)
                .FirstOrDefaultAsync(m => m.idtarif == id);
            if (mtarif == null)
            {
                return NotFound();
            }

            return View(mtarif);
        }

        // GET: Mtarifs/Create
        public IActionResult Create()
        {
            ViewData["codcat"] = new SelectList(_context.Categories, "codcat", "codcat");
            ViewData["codezone"] = new SelectList(_context.Zones, "codezone", "codezone");
            return View();
        }

        // POST: Mtarifs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("idtarif,prixkilo,codcat,codezone")] Mtarif mtarif)
        {

            if (_context.Tarifs.Any(t => t.idtarif.Equals(mtarif.idtarif)))
            {
                ViewBag.Erreur = "Ce tarif est déjà disponible ";
                ViewData["codcat"] = new SelectList(_context.Categories, "codcat", "codcat");
                ViewData["codezone"] = new SelectList(_context.Zones, "codezone", "codezone");
                return View();
            }
            _context.Add(mtarif);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        // GET: Mtarifs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mtarif = await _context.Tarifs.FindAsync(id);
            if (mtarif == null)
            {
                return NotFound();
            }
            ViewData["codcat"] = new SelectList(_context.Categories, "codcat", "codcat", mtarif.codcat);
            ViewData["codezone"] = new SelectList(_context.Zones, "codezone", "codezone", mtarif.codezone);
            return View(mtarif);
        }

        // POST: Mtarifs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("idtarif,prixkilo,codcat,codezone")] Mtarif mtarif)
        {
            if (id != mtarif.idtarif)
            {
                return NotFound();
            }

            if (id == mtarif.idtarif)
            {
                try
                {
                    _context.Update(mtarif);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MtarifExists(mtarif.idtarif))
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
            ViewData["codcat"] = new SelectList(_context.Categories, "codcat", "codcat", mtarif.codcat);
            ViewData["codezone"] = new SelectList(_context.Zones, "codezone", "codezone", mtarif.codezone);
            return View(mtarif);
        }

        // GET: Mtarifs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mtarif = await _context.Tarifs
                .Include(m => m.Categorie)
                .Include(m => m.Zone)
                .FirstOrDefaultAsync(m => m.idtarif == id);
            if (mtarif == null)
            {
                return NotFound();
            }

            return View(mtarif);
        }

        // POST: Mtarifs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var mtarif = await _context.Tarifs.FindAsync(id);
            if (mtarif != null)
            {
                _context.Tarifs.Remove(mtarif);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MtarifExists(string id)
        {
            return _context.Tarifs.Any(e => e.idtarif == id);
        }
    }
}
