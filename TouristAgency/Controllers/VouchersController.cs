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
    public class VouchersController : Controller
    {
        private readonly TouristAgencyContext _context;
        private readonly VoucherService _service;
        private readonly int _pageSize;

        public VouchersController(TouristAgencyContext context)
        {
            _context = context;
            _service = new VoucherService();
            _pageSize = 5;
        }

        // GET: Services
        public async Task<IActionResult> Index(string selectedName, int? page, VoucherService.SortState? sortState,
            int? selectedTypeOfRestId, string selectedCountry, DateTime? selectedStartDate, DateTime? selectedEndDate)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref selectedName, ref selectedTypeOfRestId, ref selectedCountry, ref selectedStartDate, ref selectedEndDate);
            _service.SetDefaultValuesIfNull(ref selectedName, ref page, ref sortState, ref selectedTypeOfRestId, ref selectedCountry, ref selectedStartDate, ref selectedEndDate);
            _service.SetCookies(Response.Cookies, User.Identity.Name, selectedName, page, sortState, selectedTypeOfRestId, selectedCountry, selectedStartDate, selectedEndDate);

            var vouchersFrequences = _context.ClientsVouchers
                .GroupBy(c => c.VoucherId)
                .Select(c => new { VoucherId = c.Key, Frequency = c.Count() });

            var vouchersViews = _context.Vouchers
                .Include(v => v.Employee)
                .Include(v => v.Hotel)
                .Include(v => v.TypeOfRest)
                .AsQueryable()
                .Select(v => new ViewModels.Voucher.VoucherViewModel
            {
                Voucher = v,
                EmployeeName = v.Employee.LastName,
                HotelName = v.Hotel.Name,
                TypeOfRestName = v.TypeOfRest.Name,
                HotelCountryName = v.Hotel.Country,
                Frequency = vouchersFrequences.FirstOrDefault(f => f.VoucherId == v.Id).Frequency
            });

            vouchersViews = _service.Filter(vouchersViews, selectedName, selectedTypeOfRestId, selectedCountry, selectedStartDate, selectedEndDate);

            var count = await vouchersViews.CountAsync();

            vouchersViews = _service.Sort(vouchersViews, (VoucherService.SortState)sortState);
            vouchersViews = _service.Paging(vouchersViews, isFromFilter, (int)page, _pageSize);

            ViewModels.Voucher.IndexVoucherViewModel model = new ViewModels.Voucher.IndexVoucherViewModel
            {
                Vouchers = await vouchersViews.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterVoucherViewModel = new ViewModels.Voucher.FilterVoucherViewModel(selectedName, selectedCountry, selectedStartDate, selectedEndDate, selectedTypeOfRestId, 
                    await _context.TypesOfRest.ToListAsync()),
                SortVoucherViewModel = new ViewModels.Voucher.SortVoucherViewModel((VoucherService.SortState)sortState)
            };

            return View(model);
        }

        // GET: Vouchers/Details/5
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

            var voucher = await _context.Vouchers
                .Include(v => v.Employee)
                .Include(v => v.Hotel)
                .Include(v => v.TypeOfRest)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (voucher == null)
            {
                return NotFound();
            }

            var serviceVouchers = _context.ServicesVouchers.Where(s => s.VoucherId == id);

            var services = _context.Services.Where(s => serviceVouchers.Select(s => s.ServiceId).Contains(s.Id));

            ViewModels.Voucher.DetailsViewModel model = new ViewModels.Voucher.DetailsViewModel
            {
                Voucher = voucher,
                Services = await services.ToListAsync()
            };

            return View(model);
        }

        // GET: Vouchers/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "LastName");
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name");
            ViewData["TypeOfRestId"] = new SelectList(_context.TypesOfRest, "Id", "Name");
            return View();
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,HotelId,TypeOfRestId,EmployeeId")] Voucher voucher)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }
            if (ModelState.IsValid)
            {
                _context.Add(voucher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "LastName", voucher.EmployeeId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name", voucher.HotelId);
            ViewData["TypeOfRestId"] = new SelectList(_context.TypesOfRest, "Id", "Name", voucher.TypeOfRestId);
            return View(voucher);
        }

        // GET: Vouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "LastName", voucher.EmployeeId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name", voucher.HotelId);
            ViewData["TypeOfRestId"] = new SelectList(_context.TypesOfRest, "Id", "Name", voucher.TypeOfRestId);
            return View(voucher);
        }

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,HotelId,TypeOfRestId,EmployeeId")] Voucher voucher)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }
            if (id != voucher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voucher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoucherExists(voucher.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "LastName", voucher.EmployeeId);
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Name", voucher.HotelId);
            ViewData["TypeOfRestId"] = new SelectList(_context.TypesOfRest, "Id", "Name", voucher.TypeOfRestId);
            return View(voucher);
        }

        // GET: Vouchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers
                .Include(v => v.Employee)
                .Include(v => v.Hotel)
                .Include(v => v.TypeOfRest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }
            var voucher = await _context.Vouchers.FindAsync(id);
            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoucherExists(int id)
        {
            return _context.Vouchers.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteService(int voucherId, int serviceId)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }
            _context.ServicesVouchers
                .Remove(await _context.ServicesVouchers
                .FirstOrDefaultAsync(s => s.VoucherId == voucherId && s.ServiceId == serviceId));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = voucherId });
        }


        [HttpGet]
        public async Task<IActionResult> AddService(int voucherId, string selectedName, int? page, ServiceService.SortState? sortState)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }

            var service = new ServiceService();

            var services = _context.Services.AsQueryable();

            services = service.Filter(services, selectedName);

            services = service.Sort(services, sortState ?? ServiceService.SortState.NameAsc);

            var count = await services.CountAsync();

            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            services = service.Paging(services, isFromFilter, page ?? 1, 5);

            ViewModels.VoucherService.IndexVoucherServiceViewModel model = new ViewModels.VoucherService.IndexVoucherServiceViewModel
            {
                VoucherId = voucherId,
                Services = await services.ToListAsync(),
                FilterServiceViewModel = new ViewModels.Service.FilterServiceViewModel(selectedName),
                SortServiceViewModel = new ViewModels.Service.SortServiceViewModel(sortState ?? ServiceService.SortState.NameAsc),
                PageViewModel = new ViewModels.PageViewModel(count, page ?? 1, _pageSize)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddService(int voucherId, int serviceId)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "Vouchers");
            }
            await _context.ServicesVouchers.AddAsync(new ServiceVoucher { VoucherId = voucherId, ServiceId = serviceId });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = voucherId });
        }
    }
}
