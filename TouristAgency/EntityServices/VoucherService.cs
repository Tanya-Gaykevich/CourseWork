using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.EntityServices
{
    public class VoucherService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc,
            OrderFrequencyAsc,
            OrderFrequencyDesc
        }
        public IQueryable<ViewModels.Voucher.VoucherViewModel> Filter(IQueryable<ViewModels.Voucher.VoucherViewModel> vouchers, string selectedName, 
            int? selectedTypeOfRestId, string selectedCountry, DateTime? selectedStartDate, DateTime? selectedEndDate)
        {
            if (!string.IsNullOrEmpty(selectedName))
            {
                vouchers = vouchers.Where(v => v.Voucher.Name.Contains(selectedName));
            }
            if(selectedTypeOfRestId != null && selectedTypeOfRestId != 0)
            {
                vouchers = vouchers.Where(v => v.Voucher.TypeOfRestId == selectedTypeOfRestId);
            }
            if(!string.IsNullOrEmpty(selectedCountry))
            {
                vouchers = vouchers.Where(v => v.HotelCountryName.Contains(selectedCountry));
            }
            if(selectedStartDate != null)
            {
                vouchers = vouchers.Where(v => v.Voucher.StartDate == selectedStartDate);
            }
            if (selectedEndDate != null)
            {
                vouchers = vouchers.Where(v => v.Voucher.EndDate == selectedEndDate);
            }
            return vouchers;
        }

        public IQueryable<ViewModels.Voucher.VoucherViewModel> Sort(IQueryable<ViewModels.Voucher.VoucherViewModel> vouchers, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    vouchers = vouchers.OrderBy(v => v.Voucher.Name);
                    break;
                case SortState.NameDesc:
                    vouchers = vouchers.OrderByDescending(v => v.Voucher.Name);
                    break;
                case SortState.OrderFrequencyAsc:
                    vouchers = vouchers.OrderBy(v => v.Frequency);
                    break;
                case SortState.OrderFrequencyDesc:
                    vouchers = vouchers.OrderByDescending(v => v.Frequency);
                    break;
            }
            return vouchers;
        }

        public IQueryable<ViewModels.Voucher.VoucherViewModel> Paging(IQueryable<ViewModels.Voucher.VoucherViewModel> vouchers, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return vouchers.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName,
            ref int? selectedTypeOfRestId, ref string selectedCountry, ref DateTime? selectedStartDate, ref DateTime? selectedEndDate)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "voucherSelectedName", out selectedName);
                }
            }
            if (selectedTypeOfRestId != null && selectedTypeOfRestId != 0)
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "voucherTypeOfRestId", out string selectedTypeOfRestIdStr);
                    selectedTypeOfRestId = int.Parse(selectedTypeOfRestIdStr);
                }
            }
            if (!string.IsNullOrEmpty(selectedCountry))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "voucherSelectedCountry", out selectedCountry);
                }
            }
            if (selectedStartDate != null)
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "voucherSelectedStartDate", out string selectedStartDateStr);
                    selectedStartDate = DateTime.Parse(selectedStartDateStr);
                }
            }
            if (selectedEndDate != null)
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "voucherSelectedEndDate", out string selectedEndDateStr);
                    selectedEndDate = DateTime.Parse(selectedEndDateStr);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if(!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "voucherPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "voucherSortState", out string sortStateStr))
                    {
                        sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                    }
                }
            }
        }

        public void SetDefaultValuesIfNull(ref string selectedName, ref int? page, ref SortState? sortState,
            ref int? selectedTypeOfRestId, ref string selectedCountry, ref DateTime? selectedStartDate, ref DateTime? selectedEndDate)
        {
            selectedName ??= "";
            page ??= 1;
            sortState ??= SortState.NameAsc;
            selectedTypeOfRestId ??= 0;
            selectedCountry ??= "";
        }

        public void SetCookies(IResponseCookies cookies, string username, string selectedName, int? page, SortState? sortState,
            int? selectedTypeOfRestId, string selectedCountry, DateTime? selectedStartDate, DateTime? selectedEndDate)
        {
            cookies.Append(username + "voucherSelectedName", selectedName);
            cookies.Append(username + "voucherPage", page.ToString());
            cookies.Append(username + "voucherSortState", sortState.ToString());
            cookies.Append(username + "voucherSelectedTypeOfRestId", selectedTypeOfRestId.ToString());
            cookies.Append(username + "voucherSelectedCountry", selectedCountry);
            cookies.Append(username + "voucherSelectedStartDate", selectedStartDate.ToString());
            cookies.Append(username + "voucherSelectedEndDate", selectedEndDate.ToString());
        }
    }
}
