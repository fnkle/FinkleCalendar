using CalendarApp.Models;
using CalendarApp.ViewModels;
using CalendarApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Utilies
{
    public class WindowService : IWindowService
    {
        public void ShowEditorWindow(EventViewModel eventVm)
        {
            var view = new EventEditorView(new EventEditorViewModel(eventVm));
            view.ShowDialog();
        }
    }
}