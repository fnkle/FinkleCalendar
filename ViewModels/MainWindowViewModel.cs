using CalendarApp.Events;
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

        public MainWindowViewModel(ICalendarEventRepository calendarEventRepository, IWindowService windowService)
        {
            _calendarEventRepository = calendarEventRepository;
            _windowService = windowService;
            Update();

            NextMonthCommand = new AppCommand(NextMonth);
            PrevMonthCommand = new AppCommand(PrevMonth);
        }

        public ObservableCollection<DayViewModel> Cells { get; } = new ObservableCollection<DayViewModel>();
        public String CurrentMonthYear => _currentDate.ToString("MMMM yyyy");

        public AppCommand NextMonthCommand { get; }

        public AppCommand PrevMonthCommand { get; }

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

            OnPropertyChanged(nameof(CurrentMonthYear));
        }

        private void ClearCells()
        {
            foreach (var cell in Cells)
            {
                cell.EventChanged -= OnEventUpdated;
            }
            Cells.Clear();
        }

        private void OnEventUpdated(object? sender, EventUpdatedEventArgs e)
        {
            var eventVm = e.Event;
            var calendarEvent = eventVm.CalendarEvent;
            switch (e.PropertyName)
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