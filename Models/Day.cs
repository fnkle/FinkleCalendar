using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class Day
    {
        private List<CalendarEvent> _events = new List<CalendarEvent>();

        public Day(int dayNumber, int monthNumber, int yearNumber)
        {
            DayNumber = dayNumber;
        }

        public List<CalendarEvent> Events => _events;
        public int DayNumber { get; set; }

        public int MonthNumber { get; set; }

        public int YearNumber { get; set; }
    }
}