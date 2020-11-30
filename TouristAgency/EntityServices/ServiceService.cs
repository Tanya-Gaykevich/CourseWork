using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.EntityServices
{
    public class ServiceService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc
        }
        public IQueryable<Models.Service> Filter(IQueryable<Models.Service> services, string selectedName)
        {
            if (!string.IsNullOrEmpty(selectedName))
            {
                services = services.Where(s => s.Name.Contains(selectedName));
            }
            return services;
        }

        public IQueryable<Models.Service> Sort(IQueryable<Models.Service> services, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    services = services.OrderBy(s => s.Name);
                    break;
                case SortState.NameDesc:
                    services = services.OrderByDescending(s => s.Name);
                    break;
            }
            return services;
        }

        public IQueryable<Models.Service> Paging(IQueryable<Models.Service> services, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return services.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "serviceSelectedName", out selectedName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if(!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "servicePage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "serviceSortState", out string sortStateStr))
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
            cookies.Append(username + "serviceSelectedName", selectedName);
            cookies.Append(username + "servicePage", page.ToString());
            cookies.Append(username + "serviceSortState", sortState.ToString());
        }
    }
}
