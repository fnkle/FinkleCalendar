using CalendarApp.Events;
using CalendarApp.Models;
using Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Utilies
{
    public interface ICalendarEventRepository
    {
        event EventHandler<EventUpdatedEventArgs> EventUpdated;

        public event EventHandler<NewEventCreatedEventArgs> EventAdded;

        void AddEvent(CalendarEvent calendarEvent);

        List<CalendarEvent> GetEventsInMonth(int month, int year);

        List<CalendarEvent> GetEventsOnDay(int day, int month, int year);

        bool TryGetEvent(Guid eventId, out CalendarEvent calendarEvent);

        List<CalendarEvent> GetAllEvents();

        bool ContainsEvent(Guid eventId);

        bool ContainsEvent(CalendarEvent calendarEvent);

        bool TryRemoveEvent(Guid eventId);

        bool TryRemoveEvent(CalendarEvent calendarEvent);
    }
}