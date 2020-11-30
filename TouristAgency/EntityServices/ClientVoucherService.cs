using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.EntityServices
{
    public class ClientVoucherService
    {
        public enum SortState
        {
            ClientLastNameAsc,
            ClientLastNameDesc
        }

        public IQueryable<Models.ClientVoucher> Filter(IQueryable<Models.ClientVoucher> clientsVouchers, string selectedClientLastName,
            bool? IsPaid, bool? IsReserved)
        {
            if (!string.IsNullOrEmpty(selectedClientLastName))
            {
                clientsVouchers = clientsVouchers.Where(c => c.Client.LastName.Contains(selectedClientLastName));
            }
            if (IsPaid ?? false)
            {
                clientsVouchers = clientsVouchers.Where(c => c.PaymentMark);
            }
            else
            {
                clientsVouchers = clientsVouchers.Where(c => !c.PaymentMark);
            }
            if (IsReserved ?? false)
            {
                clientsVouchers = clientsVouchers.Where(c => c.ReservationMark);
            }
            else
            {
                clientsVouchers = clientsVouchers.Where(c => !c.ReservationMark);
            }
            return clientsVouchers;
        }

        public IQueryable<Models.ClientVoucher> Sort(IQueryable<Models.ClientVoucher> clientsVouchers, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.ClientLastNameAsc:
                    clientsVouchers = clientsVouchers.OrderBy(c => c.Client.LastName);
                    break;
                case SortState.ClientLastNameDesc:
                    clientsVouchers = clientsVouchers.OrderByDescending(c => c.Client.LastName);
                    break;
            }
            return clientsVouchers;
        }

        public IQueryable<Models.ClientVoucher> Paging(IQueryable<Models.ClientVoucher> clientsVouchers, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return clientsVouchers.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedClientLastName,
            ref bool? isPaid, ref bool? isReserved)
        {
            if (string.IsNullOrEmpty(selectedClientLastName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "clientVoucherSelectedClientLastName", out selectedClientLastName);
                }
            }
            if (isPaid == null)
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "clientVoucherIsPaid", out string isPaidStr);
                    isPaid = !string.IsNullOrEmpty(isPaidStr) ? (bool?)bool.Parse(isPaidStr) : null;
                }
            }
            if (isReserved == null)
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "clientVoucherIsReserved", out string isReservedStr);
                    isReserved = !string.IsNullOrEmpty(isReservedStr) ? (bool?)bool.Parse(isReservedStr) : null;
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if(!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "clientVoucherPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "clientVoucherSortState", out string sortStateStr))
                    {
                        sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                    }
                }
            }
        }

        public void SetDefaultValuesIfNull(ref string selectedClientLastName, ref int? page, ref SortState? sortState, ref bool? isPaid, ref bool? isReserved)
        {
            selectedClientLastName ??= "";
            page ??= 1;
            sortState ??= SortState.ClientLastNameAsc;
            isPaid ??= false;
            isReserved ??= false;
        }

        public void SetCookies(IResponseCookies cookies, string username, string selectedClientLastName, int? page, SortState? sortState,
            ref bool? isPaid, ref bool? isReserved)
        {
            cookies.Append(username + "clientVoucherSelectedClientLastName", selectedClientLastName);
            cookies.Append(username + "clientVoucherPage", page.ToString());
            cookies.Append(username + "clientVoucherSortState", sortState.ToString());
            cookies.Append(username + "clientVoucherIsPaid", isPaid.ToString());
            cookies.Append(username + "clientVoucherIsReserved", isReserved.ToString());
        }
    }
}
