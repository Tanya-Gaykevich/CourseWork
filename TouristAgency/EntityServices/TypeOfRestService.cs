using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.EntityServices
{
    public class TypeOfRestService
    {
        public enum SortState
        {
            NameAsc,
            NameDesc
        }

        public IQueryable<Models.TypeOfRest> Filter(IQueryable<Models.TypeOfRest> typesOfRest, string selectedName)
        {
            if (!string.IsNullOrEmpty(selectedName))
            {
                typesOfRest = typesOfRest.Where(t => t.Name.Contains(selectedName));
            }
            return typesOfRest;
        }

        public IQueryable<Models.TypeOfRest> Sort(IQueryable<Models.TypeOfRest> typesOfRest, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.NameAsc:
                    typesOfRest = typesOfRest.OrderBy(t => t.Name);
                    break;
                case SortState.NameDesc:
                    typesOfRest = typesOfRest.OrderByDescending(t => t.Name);
                    break;
            }
            return typesOfRest;
        }

        public IQueryable<Models.TypeOfRest> Paging(IQueryable<Models.TypeOfRest> typesOfRest, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return typesOfRest.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref string selectedName)
        {
            if (string.IsNullOrEmpty(selectedName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "typeOfRestSelectedName", out selectedName);
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilter, ref int? page, ref SortState? sortState)
        {
            if(!isFromFilter)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "typeOfRestPage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "typeOfRestSortState", out string sortStateStr))
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
            cookies.Append(username + "typeOfRestSelectedName", selectedName);
            cookies.Append(username + "typeOfRestPage", page.ToString());
            cookies.Append(username + "typeOfRestSortState", sortState.ToString());
        }
    }
}
