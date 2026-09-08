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

        public void RequestEditWindow(Guid eventId, DateTime eventDate)
        {
            if (_eventRepo.TryGetEvent(eventId, out CalendarEvent calendarEvent))
            {
                ShowEditorWindow(calendarEvent);
            }
            else
            {
                ShowEditorWindow(new CalendarEvent(eventDate, eventDate));
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

        private void OnCloseWindow(object? sender, CloseEditorRequest e)
        {
            var window = _openWindows[e.Event.Id];

            if (window != null)
            {
                window.Close();
                _openWindows.Remove(e.Event.Id);
            }

            if (!_eventRepo.ContainsEvent(e.Event.Id))
                _eventRepo.AddEvent(e.Event);
        }
    }
}