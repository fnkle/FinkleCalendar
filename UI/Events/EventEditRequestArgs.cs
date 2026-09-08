using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Events
{
    public class EventEditRequestArgs
    {
        public Guid EventId { get; set; }

        public int Day { get; set; }
    }
}