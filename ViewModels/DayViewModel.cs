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
			DayEvents = new ObservableCollection<DayEvent>(day.Events);
		}

		public ObservableCollection<DayEvent> DayEvents { get; set; }
	}
}