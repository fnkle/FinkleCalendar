using CalendarApp.Models;
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
		private DateTime _currentDate = DateTime.Now;
		private string _currentMonthYear;

		public MainWindowViewModel()
		{
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

			for (int i = 0; i < numDays; i++)
			{
				var day = new Day();
				var dayEvent = new DayEvent();
				dayEvent.Title = "test";
				day.Events.Add(dayEvent);

				var dayEvent2 = new DayEvent();
				dayEvent2.Title = "test" + i;
				day.Events.Add(dayEvent2);
				var vm = new DayViewModel(day);
				Cells.Add(vm);
			}

			CurrentMonthYear = _currentDate.ToString("MMMM yyyy");
		}
	}
}