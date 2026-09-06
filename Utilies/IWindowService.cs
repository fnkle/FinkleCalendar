using CalendarApp.Models;
using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Utilies
{
    public interface IWindowService
    {
        void ShowEditorWindow(EventViewModel calendarEvent);
    }
}