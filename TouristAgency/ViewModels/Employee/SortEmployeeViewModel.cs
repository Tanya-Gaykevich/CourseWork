using System;
namespace TouristAgency.ViewModels.Employee
{
    public enum SortState
    {
        SurnameAsc,
        SurnameDesc,
        NameAsc,
        NameDesc,
        PatronymicAsc,
        PatronymicDesc,
        BirthdayAsc,
        BirthdayDesc
    }
    public class SortEmployeeViewModel
    {
        /// <summary>
        /// Текущее состояние сортировки
        /// </summary>
        public SortState SurnameSort { get; set; }
        public SortState NameSort { get; set; }
        public SortState PatronymicSort { get; set; }
        public SortState BirthdaySort { get; set; }
        public SortState Current { get; set; }
        public SortEmployeeViewModel(SortState sortOrder)
        {
            SurnameSort = sortOrder == SortState.SurnameAsc ? SortState.SurnameDesc : SortState.SurnameAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PatronymicSort = sortOrder == SortState.PatronymicAsc ? SortState.PatronymicDesc : SortState.PatronymicAsc;
            BirthdaySort = sortOrder == SortState.BirthdayAsc ? SortState.BirthdayDesc : SortState.BirthdayAsc;
            Current = sortOrder;
        }
    }
}
