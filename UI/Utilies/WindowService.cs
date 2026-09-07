using CalendarApp.Models;
using CalendarApp.ViewModels;
using CalendarApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using UI.Events;

namespace CalendarApp.Utilies
{
    public class WindowService : IWindowService
    {
        private readonly ICalendarEventRepository _eventRepo;
        private Dictionary<Guid, Window> _openWindows = new();

        public WindowService(ICalendarEventRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public void RequestEditWindow(Guid eventId)
        {
            var calendarEvent = _eventRepo.GetEvent(eventId);
            if (calendarEvent != null)
            {
                ShowEditorWindow(calendarEvent);
            }
        }

        private void ShowEditorWindow(CalendarEvent calendarEvent)
        {
            var vm = new EventEditorViewModel(calendarEvent);
            vm.CloseWindow += OnCloseWindow;
            var view = new EventEditorView(vm);
            _openWindows[calendarEvent.Id] = view;
            view.Show();
        }

        private void OnCloseWindow(object? sender, CloseWindowRequest e)
        {
            var window = _openWindows[e.EventId];

            if (window != null)
            {
                window.Close();
                _openWindows.Remove(e.EventId);
            }
        }
    }
}