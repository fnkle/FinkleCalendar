using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CalendarApp.ViewModels
{
	public class DayViewModel : BaseViewModel
	{
		private Day _day;

		public DayViewModel(Day day)
		{
			_day = day;
			CalendarEvents = new ObservableCollection<CalendarEvent>(day.Events);
		}

		public ObservableCollection<CalendarEvent> CalendarEvents { get; set; }

		public string DayNumber => _day.DayNumber;
	}
}