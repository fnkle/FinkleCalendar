using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CalendarApp.Models
{
    public class Day
    {
        private List<CalendarEvent> _events = new List<CalendarEvent>();

        public Day(int dayNumber, int monthNumber, int yearNumber)
        {
            DayNumber = dayNumber;
            MonthNumber = monthNumber;
            YearNumber = yearNumber;
        }

        public ReadOnlyCollection<CalendarEvent> Events => _events.AsReadOnly();
        public int DayNumber { get; set; }

        public int MonthNumber { get; set; }

        public int YearNumber { get; set; }

        public void AddEvent(CalendarEvent calendarEvent) => _events.Add(calendarEvent);

        public void RemoveEvent(CalendarEvent calendarEvent) => _events.Remove(calendarEvent);
    }
}