using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.EntityServices
{
    public class HotelService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc
        }

        public IQueryable<Models.Hotel> Filter(IQueryable<Models.Hotel> hotels, string selectedName)
        {
            if (!string.IsNullOrEmpty(selectedName))
            {
                hotels = hotels.Where(h => h.Name.Contains(selectedName));
            }
            return hotels;
        }

        public IQueryable<Models.Hotel> Sort(IQueryable<Models.Hotel> hotels, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    hotels = hotels.OrderBy(h => h.Name);
                    break;
                case SortState.NameDesc:
                    hotels = hotels.OrderByDescending(h => h.Name);
                    break;
            }
            return hotels;
        }

        public IQueryable<Models.Hotel> Paging(IQueryable<Models.Hotel> hotels, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return hotels.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "hotelSelectedName", out selectedName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if(!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "hotelPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "hotelSortState", out string sortStateStr))
                    {
                        sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                    }
                }
            }
        }

        public void SetDefaultValuesIfNull(ref string selectedName, ref int? page, ref SortState? sortState)
        {
            selectedName ??= "";
            page ??= 1;
            sortState ??= SortState.NameAsc;
        }

        public void SetCookies(IResponseCookies cookies, string username, string selectedName, int? page, SortState? sortState)
        {
            cookies.Append(username + "hotelSelectedName", selectedName);
            cookies.Append(username + "hotelPage", page.ToString());
            cookies.Append(username + "hotelSortState", sortState.ToString());
        }
    }
}
