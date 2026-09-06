using CalendarApp.Models;
using CalendarApp.Utilies;
using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace CalendarApp
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly IWindowService _windowService;
        private DateTime _currentDate = DateTime.Now;
        private string _currentMonthYear;

        public MainWindowViewModel(ICalendarEventRepository calendarEventRepository, IWindowService windowService)
        {
            _calendarEventRepository = calendarEventRepository;
            _windowService = windowService;
            _currentMonthYear = string.Empty;
            Update();
        }

        public ObservableCollection<DayViewModel> Cells { get; } = new ObservableCollection<DayViewModel>();
        public String CurrentMonthYear { get => _currentMonthYear; set => SetProperty(ref _currentMonthYear, _currentDate.ToString("MMMM yyyy")); }

        public AppCommand NextMonthCommand => new AppCommand(NextMonth);

        public AppCommand PrevMonthCommand => new AppCommand(PrevMonth);

        public int ColumnCount { get; }

        private void NextMonth()
        {
            _currentDate = _currentDate.AddMonths(1);
            Update();
        }

        private void PrevMonth()
        {
            _currentDate = _currentDate.AddMonths(-1);
            Update();
        }

        private void Update()
        {
            ClearCells();
            var numDays = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

            var eventsInMonth = _calendarEventRepository.GetEventsInMonth(_currentDate.Month, _currentDate.Year);
            for (int i = 0; i < numDays; i++)
            {
                var day = new Day(i + 1, _currentDate.Month, _currentDate.Year);
                var events = eventsInMonth.Where(calendarEvent => calendarEvent.OnDay(i + 1, _currentDate.Month, _currentDate.Year)).ToList();
                day.Events.AddRange(events);
                var vm = new DayViewModel(day, _windowService);
                vm.EventChanged += OnEventUpdated;
                Cells.Add(vm);
            }

            CurrentMonthYear = _currentDate.ToString("MMMM yyyy");
        }

        private void ClearCells()
        {
            foreach (var cell in Cells)
            {
                cell.EventChanged -= OnEventUpdated;
            }
            Cells.Clear();
        }

        private void OnEventUpdated(EventViewModel eventVm, string prop)
        {
            var calendarEvent = eventVm.CalendarEvent;
            switch (prop)
            {
                case nameof(EventViewModel.EndTime):
                case nameof(EventViewModel.StartTime):
                    Update();
                    return;

                default:
                    throw new InvalidOperationException("Property not handled in mainwindow");
            }
        }
    }
}