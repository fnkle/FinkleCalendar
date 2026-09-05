using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
	public class Day
	{
		private List<CalendarEvent> _events = new List<CalendarEvent>();

		public Day(int dayNumber)
		{
			DayNumber = dayNumber.ToString();
		}

		public List<CalendarEvent> Events => _events;
		public string DayNumber { get; set; }
	}
}