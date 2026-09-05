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
		private DateTime _currentDate = DateTime.Now;
		private string _currentMonthYear;

		public MainWindowViewModel(ICalendarEventRepository calendarEventRepository)
		{
			_calendarEventRepository = calendarEventRepository;
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
			Cells.Clear();
			var numDays = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

			var eventsInMonth = _calendarEventRepository.GetEventsInMonth(_currentDate.Month, _currentDate.Year);
			for (int i = 0; i < numDays; i++)
			{
				var day = new Day(i);
				var events = eventsInMonth.Where(calendarEvent => calendarEvent.OnDay(i, _currentDate.Month, _currentDate.Year)).ToList();
				day.Events.AddRange(events);
				Cells.Add(new DayViewModel(day));
			}

			CurrentMonthYear = _currentDate.ToString("MMMM yyyy");
		}
	}
}