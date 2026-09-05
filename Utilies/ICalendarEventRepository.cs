using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Utilies
{
	public interface ICalendarEventRepository
	{
		void AddEvent(CalendarEvent calendarEvent);

		List<CalendarEvent> GetEventsInMonth(int month, int year);

		List<CalendarEvent> GetEventsOnDay(int day, int month, int year);

		CalendarEvent GetEvent(Guid eventId);
	}
}