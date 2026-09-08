using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Events
{
    public class CloseEditorRequest
    {
        public bool DataSaved;

        public CalendarEvent Event;
    }
}