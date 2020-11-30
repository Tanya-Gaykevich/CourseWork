using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TouristAgency.Data;
using TouristAgency.EntityServices;
using TouristAgency.Models;

namespace TouristAgency.Controllers
{
    public class TypesOfRestController : Controller
    {
        private readonly TouristAgencyContext _context;
        private readonly TypeOfRestService _service;
        private readonly int _pageSize;

        public TypesOfRestController(TouristAgencyContext context)
        {
            _context = context;
            _service = new TypeOfRestService();
            _pageSize = 5;
        }

        // GET: Services
        public async Task<IActionResult> Index(string selectedName, int? page, TypeOfRestService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref selectedName);
            _service.SetDefaultValuesIfNull(ref selectedName, ref page, ref sortState);
            _service.SetCookies(Response.Cookies, User.Identity.Name, selectedName, page, sortState);

            var typesOfRest = _context.TypesOfRest.AsQueryable();

            typesOfRest = _service.Filter(typesOfRest, selectedName);

            var count = await typesOfRest.CountAsync();

            typesOfRest = _service.Sort(typesOfRest, (TypeOfRestService.SortState)sortState);
            typesOfRest = _service.Paging(typesOfRest, isFromFilter, (int)page, _pageSize);

            ViewModels.TypeOfRest.IndexTypeOfRestViewModel model = new ViewModels.TypeOfRest.IndexTypeOfRestViewModel
            {
                TypesOfRest = await typesOfRest.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterTypeOfRestViewModel = new ViewModels.TypeOfRest.FilterTypeOfRestViewModel(selectedName),
                SortTypeOfRestViewModel = new ViewModels.TypeOfRest.SortTypeOfRestViewModel((TypeOfRestService.SortState)sortState)
            };

            return View(model);
        }

        // GET: TypesOfRest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var typeOfRest = await _context.TypesOfRest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfRest == null)
            {
                return NotFound();
            }

            return View(typeOfRest);
        }

        // GET: TypesOfRest/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "TypesOfRest");
            }
            return View();
        }

        // POST: TypesOfRest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Limitation")] TypeOfRest typeOfRest)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "TypesOfRest");
            }
            if (ModelState.IsValid)
            {
                _context.Add(typeOfRest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfRest);
        }

        // GET: TypesOfRest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "TypesOfRest");
            }
            if (id == null)
            {
                return NotFound();
            }

            var typeOfRest = await _context.TypesOfRest.FindAsync(id);
            if (typeOfRest == null)
            {
                return NotFound();
            }
            return View(typeOfRest);
        }

        // POST: TypesOfRest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Limitation")] TypeOfRest typeOfRest)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "TypesOfRest");
            }
            if (id != typeOfRest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfRest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfRestExists(typeOfRest.Id))
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
            return View(typeOfRest);
        }

        // GET: TypesOfRest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "TypesOfRest");
            }
            if (id == null)
            {
                return NotFound();
            }

            var typeOfRest = await _context.TypesOfRest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeOfRest == null)
            {
                return NotFound();
            }

            return View(typeOfRest);
        }

        // POST: TypesOfRest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "TypesOfRest");
            }
            var typeOfRest = await _context.TypesOfRest.FindAsync(id);
            _context.TypesOfRest.Remove(typeOfRest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfRestExists(int id)
        {
            return _context.TypesOfRest.Any(e => e.Id == id);
        }
    }
}
