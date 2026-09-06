using CalendarApp.Models;
using CalendarApp.ViewModels;
using CalendarApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CalendarApp.Utilies
{
    public class WindowService : IWindowService
    {
        private Dictionary<EventViewModel, Window> _openWindows = new();

        public void CloseEditorWindow(EventViewModel eventViewModel)
        {
            if (_openWindows.TryGetValue(eventViewModel, out Window? window))
            {
                window.Close();
                _openWindows.Remove(eventViewModel);
            }
        }

        public void ShowEditorWindow(EventViewModel eventVm)
        {
            var view = new EventEditorView(new EventEditorViewModel(eventVm, this));
            _openWindows[eventVm] = view;
            view.Show();
        }
    }
}