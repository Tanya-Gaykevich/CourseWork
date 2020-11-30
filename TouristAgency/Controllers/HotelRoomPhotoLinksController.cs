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
    public class HotelRoomPhotoLinksController : Controller
    {
        private readonly TouristAgencyContext _context;
        private readonly HotelRoomPhotoLinkService _service;
        private readonly int _pageSize;

        public HotelRoomPhotoLinksController(TouristAgencyContext context)
        {
            _context = context;
            _service = new HotelRoomPhotoLinkService();
            _pageSize = 8;
        }

        // GET: HotelRoomPhotoLinks
        public async Task<IActionResult> Index(string selectedHotelName, int? page, HotelRoomPhotoLinkService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }

            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref selectedHotelName);
            _service.SetDefaultValuesIfNull(ref selectedHotelName, ref page, ref sortState);
            _service.SetCookies(Response.Cookies, User.Identity.Name, selectedHotelName, page, sortState);

            var hotelRoomPhotoLinks = _context.HotelRoomPhotoLinks.Include(h => h.Hotel).AsQueryable();

            hotelRoomPhotoLinks = _service.Filter(hotelRoomPhotoLinks, selectedHotelName);

            var count = await hotelRoomPhotoLinks.CountAsync();

            hotelRoomPhotoLinks = _service.Sort(hotelRoomPhotoLinks, (HotelRoomPhotoLinkService.SortState)sortState);
            hotelRoomPhotoLinks = _service.Paging(hotelRoomPhotoLinks, isFromFilter, (int)page, _pageSize);

            ViewModels.HotelRoomPhotoLink.IndexHotelRoomPhotoLinkViewModel model = new ViewModels.HotelRoomPhotoLink.IndexHotelRoomPhotoLinkViewModel
            {
                HotelRoomPhotoLinks = await hotelRoomPhotoLinks.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterHotelRoomPhotoLinkViewModel = new ViewModels.HotelRoomPhotoLink.FilterHotelRoomPhotoLinkViewModel(selectedHotelName),
                SortHotelRoomPhotoLinkViewModel = new ViewModels.HotelRoomPhotoLink.SortHotelRoomPhotoLinkViewModel((HotelRoomPhotoLinkService.SortState)sortState),
            };

            return View(model);
        }

        // GET: HotelRoomPhotoLinks/Details/5
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

            var hotelRoomPhotoLink = await _context.HotelRoomPhotoLinks
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelRoomPhotoLink == null)
            {
                return NotFound();
            }

            return View(hotelRoomPhotoLink);
        }

        // GET: HotelRoomPhotoLinks/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "HotelRoomPhotoLinks");
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name");
            return View();
        }

        // POST: HotelRoomPhotoLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HotelId,RoomNumber,RoomPhotoLink")] HotelRoomPhotoLink hotelRoomPhotoLink)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "HotelRoomPhotoLinks");
            }
            if (ModelState.IsValid)
            {
                _context.Add(hotelRoomPhotoLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name", hotelRoomPhotoLink.HotelId);
            return View(hotelRoomPhotoLink);
        }

        // GET: HotelRoomPhotoLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "HotelRoomPhotoLinks");
            }
            if (id == null)
            {
                return NotFound();
            }

            var hotelRoomPhotoLink = await _context.HotelRoomPhotoLinks.FindAsync(id);
            if (hotelRoomPhotoLink == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name", hotelRoomPhotoLink.HotelId);
            return View(hotelRoomPhotoLink);
        }

        // POST: HotelRoomPhotoLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HotelId,RoomNumber,RoomPhotoLink")] HotelRoomPhotoLink hotelRoomPhotoLink)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "HotelRoomPhotoLinks");
            }
            if (id != hotelRoomPhotoLink.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelRoomPhotoLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelRoomPhotoLinkExists(hotelRoomPhotoLink.Id))
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
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name", hotelRoomPhotoLink.HotelId);
            return View(hotelRoomPhotoLink);
        }

        // GET: HotelRoomPhotoLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "HotelRoomPhotoLinks");
            }
            if (id == null)
            {
                return NotFound();
            }

            var hotelRoomPhotoLink = await _context.HotelRoomPhotoLinks
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotelRoomPhotoLink == null)
            {
                return NotFound();
            }

            return View(hotelRoomPhotoLink);
        }

        // POST: HotelRoomPhotoLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "HotelRoomPhotoLinks");
            }
            var hotelRoomPhotoLink = await _context.HotelRoomPhotoLinks.FindAsync(id);
            _context.HotelRoomPhotoLinks.Remove(hotelRoomPhotoLink);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelRoomPhotoLinkExists(int id)
        {
            return _context.HotelRoomPhotoLinks.Any(e => e.Id == id);
        }
    }
}
