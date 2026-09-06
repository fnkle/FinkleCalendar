using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Events
{
    public class EventUpdatedEventArgs
    {
        public EventViewModel Event { get; set; }
        public string PropertyName { get; set; } = string.Empty;
    }
}