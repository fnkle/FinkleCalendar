using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
	public class Day
	{
		private List<DayEvent> _events = new List<DayEvent>();

		public List<DayEvent> Events => _events;
	}
}