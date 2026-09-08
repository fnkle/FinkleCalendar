using CalendarApp.Events;
using CalendarApp.Models;
using Core.Events;
using Core.Persistence;

namespace CalendarApp.Utilies
{
    public class CalendarEventRepository : ICalendarEventRepository
    {
        private Dictionary<Guid, CalendarEvent> _events = new Dictionary<Guid, CalendarEvent>();

        public CalendarEventRepository(ICalendarEventPersister persister)
        {
            persister.LoadEvents().ForEach(calendarEvent =>
            {
                calendarEvent.PropertyChanged += OnEventPropertyChanged;
                _events.Add(calendarEvent.Id, calendarEvent);
            });

            if (_events.Count == 0)
            {
                var calendarEvent = new CalendarEvent(DateTime.Now, DateTime.Now.AddDays(3));
                calendarEvent.Title = "test";

                _events.Add(calendarEvent.Id, calendarEvent);
            }
        }

        public event EventHandler<EventUpdatedEventArgs> EventUpdated;

        public event EventHandler<NewEventCreatedEventArgs> EventAdded;

        public void AddEvent(CalendarEvent calendarEvent)
        {
            if (_events.TryGetValue(calendarEvent.Id, out var existingEvent))
            {
                existingEvent.PropertyChanged -= OnEventPropertyChanged;
            }

            _events[calendarEvent.Id] = calendarEvent;
            calendarEvent.PropertyChanged += OnEventPropertyChanged;

            EventAdded.Invoke(this, new NewEventCreatedEventArgs { EventId = calendarEvent.Id });
        }

        public bool ContainsEvent(Guid eventId) => _events.ContainsKey(eventId);

        public bool ContainsEvent(CalendarEvent calendarEvent) => ContainsEvent(calendarEvent.Id);

        public List<CalendarEvent> GetAllEvents() => _events.Values.ToList();

        public bool TryGetEvent(Guid eventId, out CalendarEvent calendarEvent)
        {
            calendarEvent = null;

            if (_events.ContainsKey(eventId))
            {
                calendarEvent = _events[eventId];
                return true;
            }

            return false;
        }

        public List<CalendarEvent> GetEventsInMonth(int month, int year)
        {
            return _events.Values.Where(calendarEvent => calendarEvent.InMonth(month, year)).ToList();
        }

        public List<CalendarEvent> GetEventsOnDay(int day, int month, int year)
        {
            return _events.Values.Where(calendarEvent => calendarEvent.OnDay(day, month, year)).ToList();
        }

        public bool TryRemoveEvent(Guid eventId)
        {
            return _events.Remove(eventId);
        }

        public bool TryRemoveEvent(CalendarEvent calendarEvent) => TryRemoveEvent(calendarEvent.Id);

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