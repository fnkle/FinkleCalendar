using CalendarApp.Events;
using CalendarApp.Models;
using CalendarApp.Utilies;
using CalendarApp.ViewModels;
using Core.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;
using UI.Events;

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
            _calendarEventRepository.EventUpdated += OnEventUpdated;
            _calendarEventRepository.EventAdded += OnEventAdded;
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
            Cells.Clear();
            var numDays = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

            var eventsInMonth = _calendarEventRepository.GetEventsInMonth(_currentDate.Month, _currentDate.Year);
            for (int i = 0; i < numDays; i++)
            {
                var events = eventsInMonth.Where(calendarEvent => calendarEvent.OnDay(i + 1, _currentDate.Month, _currentDate.Year)).ToList();
                var vm = new DayViewModel(events, i + 1);
                vm.EventEditRequest += OnEventEditRequested;
                Cells.Add(vm);
            }

            OnPropertyChanged(nameof(CurrentMonthYear));
        }

        private void OnEventEditRequested(object? sender, EventEditRequestArgs e) => _windowService.RequestEditWindow(e.EventId, new DateTime(day: e.Day, month: _currentDate.Month, year: _currentDate.Year, hour: 0, minute: 0, second: 0));

        private void OnEventUpdated(object? sender, EventUpdatedEventArgs e)
        {
            Update();
        }

        private void OnEventAdded(object? sender, NewEventCreatedEventArgs e)
        {
            Update();
        }
    }
}