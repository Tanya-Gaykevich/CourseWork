using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.EntityServices
{
    public class HotelRoomPhotoLinkService
    {
        public enum SortState
        {
            HotelNameAsc,
            HotelNameDesc
        }

        public IQueryable<Models.HotelRoomPhotoLink> Filter(IQueryable<Models.HotelRoomPhotoLink> hotelRoomPhotoLinks, string selectedHotelName)
        {
            if (!string.IsNullOrEmpty(selectedHotelName))
            {
                hotelRoomPhotoLinks = hotelRoomPhotoLinks.Where(h => h.Hotel.Name.Contains(selectedHotelName));
            }
            return hotelRoomPhotoLinks;
        }

        public IQueryable<Models.HotelRoomPhotoLink> Sort(IQueryable<Models.HotelRoomPhotoLink> hotelRoomPhotoLinks, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.HotelNameAsc:
                    hotelRoomPhotoLinks = hotelRoomPhotoLinks.OrderBy(c => c.Hotel.Name);
                    break;
                case SortState.HotelNameDesc:
                    hotelRoomPhotoLinks = hotelRoomPhotoLinks.OrderByDescending(c => c.Hotel.Name);
                    break;
            }
            return hotelRoomPhotoLinks;
        }

        public IQueryable<Models.HotelRoomPhotoLink> Paging(IQueryable<Models.HotelRoomPhotoLink> hotelRoomPhotoLinks, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return hotelRoomPhotoLinks.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedHotelName)
        {
            if (string.IsNullOrEmpty(selectedHotelName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "roomSelectedHotelName", out selectedHotelName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref int? page, ref SortState? sortState)
        {
            if(!isFromFilterForm)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "roomPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "roomSortState", out string sortStateStr))
                    {
                        sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                    }
                }
            }
        }

        public void SetDefaultValuesIfNull(ref string selectedHotelName, ref int? page, ref SortState? sortState)
        {
            selectedHotelName ??= "";
            page ??= 1;
            sortState ??= SortState.HotelNameAsc;
        }

        public void SetCookies(IResponseCookies cookies, string username, string selectedHotelName, int? page, SortState? sortState)
        {
            cookies.Append(username + "roomSelectedHotelName", selectedHotelName);
            cookies.Append(username + "roomPage", page.ToString());
            cookies.Append(username + "roomSortState", sortState.ToString());
        }
    }
}
