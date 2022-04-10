using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nyilvantartas.Data;
using nyilvantartas.Models;

namespace nyilvantartas.Controllers
{
    public class arukController : Controller
    {
        private readonly ApplicationDbContext _context;

        public arukController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: aruk
        public async Task<IActionResult> Index (string keresMegnevezes, string keresTipus)
        {
            var aruk = _context.aruk.Select(x => x);
            if (!string.IsNullOrEmpty(keresMegnevezes))
            {
                aruk = aruk.Where(aru => aru.Megnevezes.Contains(keresMegnevezes));
            }

            if (!string.IsNullOrEmpty(keresTipus))
            {
                aruk = aruk.Where(aru => aru.Tipus.Equals(keresTipus));
            }

            var Megnevezes = _context.aruk.Select(Aru => Aru.Megnevezes).Distinct();
            var Tipus = _context.aruk.Select(Aru => Aru.Tipus).Distinct();

            AruViewModel modelkereseshez = new AruViewModel();
            modelkereseshez.Tipus = new SelectList(await Tipus.ToListAsync());
            modelkereseshez.keresMegnevezes = keresMegnevezes;
            modelkereseshez.nyilvantartas = await aruk.ToListAsync();




            return View(modelkereseshez);
        }

        // GET: aruk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aruk = await _context.aruk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aruk == null)
            {
                return NotFound();
            }

            return View(aruk);
        }

        [Authorize]
        // GET: aruk/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: aruk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Megnevezes,Gyarto,Tipus,BeszerzesiAr,Id")] aruk aruk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aruk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aruk);
        }
        [Authorize]
        // GET: aruk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aruk = await _context.aruk.FindAsync(id);
            if (aruk == null)
            {
                return NotFound();
            }
            return View(aruk);
        }

        // POST: aruk/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Megnevezes,Gyarto,Tipus,BeszerzesiAr,Id")] aruk aruk)
        {
            if (id != aruk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aruk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!arukExists(aruk.Id))
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
            return View(aruk);
        }
        [Authorize]
        // GET: aruk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aruk = await _context.aruk
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aruk == null)
            {
                return NotFound();
            }

            return View(aruk);
        }

        // POST: aruk/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aruk = await _context.aruk.FindAsync(id);
            _context.aruk.Remove(aruk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool arukExists(int id)
        {
            return _context.aruk.Any(e => e.Id == id);
        }
    }
}
