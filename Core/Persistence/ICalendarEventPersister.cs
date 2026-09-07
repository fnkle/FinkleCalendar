using CalendarApp.Models;

namespace Core.Persistence
{
    public interface ICalendarEventPersister
    {
        List<CalendarEvent> LoadEvents();

        void SaveEvents(List<CalendarEvent> events);
    }
}