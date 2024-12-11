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
    public class McategoriesController : Controller
    {
        private readonly AnnuaireContext _context;

        public McategoriesController(AnnuaireContext context)
        {
            _context = context;
        }

        // GET: Mcategories
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }

        // GET: Mcategories/Details/5
        

        // GET: Mcategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Mcategorie mcategorie)
        {
            if ( _context.Categories.Any(c => c.codcat.Equals(mcategorie.codcat)) && (_context.Categories.Any(c => c.designcat.Equals(mcategorie.designcat))))
            {
                ViewBag.Erreur = "Ce catégorie existe déjà";
                return View();
            }
            _context.Add(mcategorie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Mcategories/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mcategorie = _context.Categories.Find(id);
            if (mcategorie == null)
            {
                return NotFound();
            }
            return View(mcategorie);
        }

        // POST: Mcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Mcategorie mcategorie)
        {
            if (id != mcategorie.codcat)
            {
                return NotFound();
            }
            
            if (_context.Categories.Any(c => c.codcat.Equals(id)))
            {
                try
                {
                    _context.Update(mcategorie);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!McategorieExists(mcategorie.codcat))
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
            

            return View();
        
            
        }

        // GET: Mcategories/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mcategorie = _context.Categories
                .FirstOrDefault(m => m.codcat == id);
            if (mcategorie == null)
            {
                return NotFound();
            }

            return View(mcategorie);
        }

        // POST: Mcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var mcategorie = _context.Categories.Find(id);
            if (mcategorie != null)
            {
                _context.Categories.Remove(mcategorie);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool McategorieExists(string id)
        {
            return _context.Categories.Any(e => e.codcat == id);
        }
    }
}
