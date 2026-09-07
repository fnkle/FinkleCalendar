using System;

namespace CalendarApp.Events
{
    public class EventUpdatedEventArgs : EventArgs
    {
        public Guid EventId { get; set; }
        public string PropertyName { get; set; } = string.Empty;
    }
}
