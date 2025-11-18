using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTagHelper.Data;
using MVCTagHelper.Models;
using MVCTagHelper.ViewModels;

namespace MVCTagHelper.Controllers
{
    public class AfdelingenController : Controller
    {
        private readonly TagHelperDbContext _context;

        public AfdelingenController(TagHelperDbContext context)
        {
            _context = context;
        }

        // GET: Afdelingen
        public async Task<IActionResult> Index()
        {
            var tagHelperDbContext = _context.Afdelingen.Include(a => a.Locatie);
            return View(await tagHelperDbContext.ToListAsync());
        }

        // GET: Afdelingen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Afdelingen == null)
            {
                return NotFound();
            }

            var afdeling = await _context.Afdelingen
                .Include(a => a.Locatie)
                .ThenInclude(l => l.Land)
                .FirstOrDefaultAsync(m => m.AfdelingId == id);
            if (afdeling == null)
            {
                return NotFound();
            }

            return View(afdeling);
        }

        // GET: Afdelingen/Create
        public IActionResult Create()
        {
            ViewData["LocatieId"] = new SelectList(_context.Locaties, "LocatieId", "LocatieId");
            return View();
        }

        // POST: Afdelingen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AfdelingId,AfdelingNaam,LocatieId")] Afdeling afdeling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(afdeling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocatieId"] = new SelectList(_context.Locaties, "LocatieId", "LocatieId", afdeling.LocatieId);
            return View(afdeling);
        }

        // GET: Afdelingen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Afdelingen == null)
            {
                return NotFound();
            }

            var afdeling = await _context.Afdelingen.FindAsync(id);
            if (afdeling == null)
            {
                return NotFound();
            }
            ViewData["LocatieId"] = new SelectList(_context.Locaties, "LocatieId", "LocatieId", afdeling.LocatieId);
            return View(afdeling);
        }

        // POST: Afdelingen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AfdelingId,AfdelingNaam,LocatieId")] Afdeling afdeling)
        {
            if (id != afdeling.AfdelingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(afdeling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AfdelingExists(afdeling.AfdelingId))
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
            ViewData["LocatieId"] = new SelectList(_context.Locaties, "LocatieId", "LocatieId", afdeling.LocatieId);
            return View(afdeling);
        }

        // GET: Afdelingen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Afdelingen == null)
            {
                return NotFound();
            }

            var afdeling = await _context.Afdelingen
                .Include(a => a.Locatie)
                .FirstOrDefaultAsync(m => m.AfdelingId == id);
            if (afdeling == null)
            {
                return NotFound();
            }

            return View(afdeling);
        }

        // POST: Afdelingen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Afdelingen == null)
            {
                return Problem("Entity set 'TagHelperDbContext.Afdelingen'  is null.");
            }
            var afdeling = await _context.Afdelingen.FindAsync(id);
            if (afdeling != null)
            {
                _context.Afdelingen.Remove(afdeling);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SearchFormAfdelingen()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchFormAfdelingen(SearchViewModel vm)
        {
            var afdeling = _context.Afdelingen
                .Include(a => a.Locatie)
                    .ThenInclude(l => l.Land)
                .Where(x => x.AfdelingNaam.Contains(vm.SearchName)).ToList();   
            if (afdeling.Count == 0)
            {
                  return View();
            }

            var results = new SearchResultsViewModel();
            results.Afdelingen = afdeling;
            results.AfdelingLandInfo = AfdelingLandViewModel(afdeling);
            return View("SearchResults", results);
        }

        private IEnumerable<AfdelingLandViewModel> AfdelingLandViewModel(IEnumerable<Afdeling> afd)
        {
            var info = afd.Select(x => new AfdelingLandViewModel()
            {
                AfdelingId = x.AfdelingId,
                AfdelingNaam = x.AfdelingNaam,
                Land = x.Locatie?.Land?.LandNaam
            });

            return info;

        }

        public IActionResult SearchResults(IEnumerable<Afdeling> afd)
        {
            var vm = new SearchResultsViewModel();
            vm.AfdelingLandInfo = (IEnumerable<AfdelingLandViewModel>?)afd;
            return View(vm);
        }


        private bool AfdelingExists(int id)
        {
          return _context.Afdelingen.Any(e => e.AfdelingId == id);
        }
    }
}
