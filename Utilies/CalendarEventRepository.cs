using CalendarApp.Models;

namespace CalendarApp.Utilies
{
    public class CalendarEventRepository : ICalendarEventRepository
    {
        private Dictionary<Guid, CalendarEvent> _events = new Dictionary<Guid, CalendarEvent>();

        public CalendarEventRepository()
        {
            var calendarEvent = new CalendarEvent(DateTime.Now, DateTime.Now.AddDays(3));
            calendarEvent.Title = "test";

            AddEvent(calendarEvent);
        }

        public void AddEvent(CalendarEvent calendarEvent)
        {
            _events[calendarEvent.Id] = calendarEvent;
        }

        public CalendarEvent? GetEvent(Guid eventId)
        {
            if (_events.ContainsKey(eventId))
            {
                return _events[eventId];
            }

            return null;
        }

        public List<CalendarEvent> GetEventsInMonth(int month, int year)
        {
            return _events.Values.Where(calendarEvent => calendarEvent.InMonth(month, year)).ToList();
        }

        public List<CalendarEvent> GetEventsOnDay(int day, int month, int year)
        {
            return _events.Values.Where(calendarEvent => calendarEvent.OnDay(day, month, year)).ToList();
        }
    }
}