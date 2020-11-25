using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.ViewModels.Client
{
    public enum SortState
    {
        BirthdayAsc,
        BirthdayDesc
    }
    public class SortClientViewModel
    {
        public SortState BirthdaySort { get; set; }
        public SortState Current { get; set; }
        public SortClientViewModel(SortState sortOrder)
        {
            BirthdaySort = sortOrder == SortState.BirthdayAsc ? SortState.BirthdayDesc : SortState.BirthdayAsc;
            Current = sortOrder;
        }
    }
}
