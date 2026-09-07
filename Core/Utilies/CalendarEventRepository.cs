using CalendarApp.Events;
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

        public event EventHandler<EventUpdatedEventArgs>? EventUpdated;

        public void AddEvent(CalendarEvent calendarEvent)
        {
            if (_events.TryGetValue(calendarEvent.Id, out var existingEvent))
            {
                existingEvent.PropertyChanged -= OnEventPropertyChanged;
            }

            _events[calendarEvent.Id] = calendarEvent;
            calendarEvent.PropertyChanged += OnEventPropertyChanged;
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

        private void OnEventPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is CalendarEvent calendarEvent)
            {
                EventUpdated?.Invoke(this, new EventUpdatedEventArgs
                {
                    EventId = calendarEvent.Id,
                    PropertyName = e.PropertyName ?? string.Empty
                });
            }
        }
    }
}