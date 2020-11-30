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
    public class ClientsVouchersController : Controller
    {
        private readonly TouristAgencyContext _context;
        private readonly ClientVoucherService _service;
        private readonly int _pageSize;

        public ClientsVouchersController(TouristAgencyContext context)
        {
            _context = context;
            _service = new ClientVoucherService();
            _pageSize = 5;
        }

        // GET: Services
        public async Task<IActionResult> Index(string selectedClientLastName, int? page, ClientVoucherService.SortState? sortState, bool? isPaid, bool? isReserved)
        {
            if (!User.IsInRole(Areas.Identity.Roles.User) && !User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return Redirect("~/Identity/Account/Login");
            }
            bool isFromFilter = HttpContext.Request.Query["isFromFilter"] == "true";

            _service.GetSortPagingCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref page, ref sortState);
            _service.GetFilterCookiesForUserIfNull(Request.Cookies, User.Identity.Name, isFromFilter,
                ref selectedClientLastName, ref isPaid, ref isReserved);
            _service.SetDefaultValuesIfNull(ref selectedClientLastName, ref page, ref sortState, ref isPaid, ref isReserved);
            _service.SetCookies(Response.Cookies, User.Identity.Name, selectedClientLastName, page, sortState, ref isPaid, ref isReserved);

            var clientsVouchers = _context.ClientsVouchers.Include(c => c.Client).Include(c => c.Voucher).AsQueryable();

            clientsVouchers = _service.Filter(clientsVouchers, selectedClientLastName, isPaid, isReserved);

            var count = await clientsVouchers.CountAsync();

            clientsVouchers = _service.Sort(clientsVouchers, (ClientVoucherService.SortState)sortState);
            clientsVouchers = _service.Paging(clientsVouchers, isFromFilter, (int)page, _pageSize);

            ViewModels.ClientVoucher.IndexClientVoucherViewModel model = new ViewModels.ClientVoucher.IndexClientVoucherViewModel
            {
                ClientsVouchers = await clientsVouchers.ToListAsync(),
                PageViewModel = new ViewModels.PageViewModel(count, (int)page, _pageSize),
                FilterClientVoucherViewModel = new ViewModels.ClientVoucher.FilterClientVoucherViewModel(selectedClientLastName, isPaid, isReserved),
                SortClientVoucherViewModel = new ViewModels.ClientVoucher.SortClientVoucherViewModel((ClientVoucherService.SortState)sortState)
            };

            return View(model);
        }

        // GET: ClientVouchers/Details/5
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

            var clientVoucher = await _context.ClientsVouchers
                .Include(c => c.Client)
                .Include(c => c.Voucher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientVoucher == null)
            {
                return NotFound();
            }

            return View(clientVoucher);
        }

        // GET: ClientVouchers/Create
        public IActionResult Create()
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "ClientsVouchers");
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "LastName");
            ViewData["VoucherId"] = new SelectList(_context.Vouchers, "Id", "Name");
            return View();
        }

        // POST: ClientVouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReservationMark,PaymentMark,VoucherId,ClientId")] ClientVoucher clientVoucher)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "ClientsVouchers");
            }
            if (ModelState.IsValid)
            {
                _context.Add(clientVoucher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "LastName", clientVoucher.ClientId);
            ViewData["VoucherId"] = new SelectList(_context.Vouchers, "Id", "Name", clientVoucher.VoucherId);
            return View(clientVoucher);
        }

        // GET: ClientVouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "ClientsVouchers");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clientVoucher = await _context.ClientsVouchers.FindAsync(id);
            if (clientVoucher == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "LastName", clientVoucher.ClientId);
            ViewData["VoucherId"] = new SelectList(_context.Vouchers, "Id", "Name", clientVoucher.VoucherId);
            return View(clientVoucher);
        }

        // POST: ClientVouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReservationMark,PaymentMark,VoucherId,ClientId")] ClientVoucher clientVoucher)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "ClientsVouchers");
            }
            if (id != clientVoucher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientVoucher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientVoucherExists(clientVoucher.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "LastName", clientVoucher.ClientId);
            ViewData["VoucherId"] = new SelectList(_context.Vouchers, "Id", "Name", clientVoucher.VoucherId);
            return View(clientVoucher);
        }

        // GET: ClientVouchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "ClientsVouchers");
            }
            if (id == null)
            {
                return NotFound();
            }

            var clientVoucher = await _context.ClientsVouchers
                .Include(c => c.Client)
                .Include(c => c.Voucher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientVoucher == null)
            {
                return NotFound();
            }

            return View(clientVoucher);
        }

        // POST: ClientVouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(Areas.Identity.Roles.Admin))
            {
                return RedirectToAction("Index", "ClientsVouchers");
            }
            var clientVoucher = await _context.ClientsVouchers.FindAsync(id);
            _context.ClientsVouchers.Remove(clientVoucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientVoucherExists(int id)
        {
            return _context.ClientsVouchers.Any(e => e.Id == id);
        }
    }
}
