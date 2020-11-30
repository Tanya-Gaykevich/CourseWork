using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.EntityServices
{
    public class EmployeeService
    {
        public enum SortState
        {
            LastNameAsc,
            LastNameDesc
        }

        public IQueryable<Models.Employee> Filter(IQueryable<Models.Employee> employees, string selectedLastName, int? selectedAge, int? selectedPositionId)
        {
            if (!string.IsNullOrEmpty(selectedLastName))
            {
                employees = employees.Where(e => e.LastName.Contains(selectedLastName));
            }
            if (selectedAge != null)
            {
                employees = employees.Where(e => DateTime.Now.Year - e.BirthDate.Year - (DateTime.Now <= e.BirthDate.AddYears(DateTime.Now.Year - e.BirthDate.Year) ? 0 : 1) == selectedAge);
            }
            if (selectedPositionId != null && selectedPositionId != 0)
            {
                employees = employees.Where(e => e.PositionId == selectedPositionId);
            }
            return employees;
        }

        public IQueryable<Models.Employee> Sort(IQueryable<Models.Employee> employees, SortState sortState)
        {
            switch (sortState)
            {
                case SortState.LastNameAsc:
                    employees = employees.OrderBy(e => e.LastName);
                    break;
                case SortState.LastNameDesc:
                    employees = employees.OrderByDescending(e => e.LastName);
                    break;
            }
            return employees;
        }

        public IQueryable<Models.Employee> Paging(IQueryable<Models.Employee> employees, bool isFromFilter, int page, int pageSize)
        {
            if (isFromFilter)
            {
                page = 1;
            }
            return employees.Skip(((int)page - 1) * pageSize).Take(pageSize);
        }

        public void GetFilterCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, 
            ref string selectedLastName, ref int? selectedAge, ref int? selectedPositionId)
        {
            if (string.IsNullOrEmpty(selectedLastName))
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "employeeSelectedLastName", out selectedLastName);
                }
            }
            if(selectedAge == null)
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "employeeSelectedAge", out string selectedAgeStr);
                    selectedAge = !string.IsNullOrEmpty(selectedAgeStr) ? (int?)int.Parse(selectedAgeStr) : null;
                }
            }
            if(selectedPositionId == null || selectedPositionId == 0)
            {
                if (!isFromFilterForm)
                {
                    cookies.TryGetValue(username + "employeeSelectedPositionId", out string selectedPositionIdStr);
                    selectedPositionId = !string.IsNullOrEmpty(selectedPositionIdStr) ? (int?)int.Parse(selectedPositionIdStr) : null;
                }
            }
        }

        public void GetSortPagingCookiesForUserIfNull(IRequestCookieCollection cookies, string username, bool isFromFilterForm, ref int? page, ref SortState? sortState)
        {
            if(!isFromFilterForm)
            {
                if (page == null)
                {
                    if (cookies.TryGetValue(username + "employeePage", out string pageStr))
                    {
                        page = int.Parse(pageStr);
                    }
                }
                if (sortState == null)
                {
                    if (cookies.TryGetValue(username + "employeeSortState", out string sortStateStr))
                    {
                        sortState = (SortState)Enum.Parse(typeof(SortState), sortStateStr);
                    }
                }
            }
        }

        public void SetDefaultValuesIfNull(ref string selectedLastName, ref int? page, ref SortState? sortState, 
            ref int? selectedAge, ref int? selectedPositionId)
        {
            selectedLastName ??= "";
            page ??= 1;
            sortState ??= SortState.LastNameAsc;
            selectedPositionId ??= 0;
        }

        public void SetCookies(IResponseCookies cookies, string username, string selectedLastName, int? page, SortState? sortState,
            int? selectedAge, int? selectedPositionId)
        {
            cookies.Append(username + "employeeSelectedLastName", selectedLastName);
            cookies.Append(username + "employeePage", page.ToString());
            cookies.Append(username + "employeeSortState", sortState.ToString());
            cookies.Append(username + "employeeSelectedAge", selectedAge.ToString());
            cookies.Append(username + "employeeSelectedPositionId", selectedPositionId.ToString());
        }
    }
}
