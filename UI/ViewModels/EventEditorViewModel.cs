using CalendarApp.Events;
using CalendarApp.Models;
using CalendarApp.Utilies;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Events;

namespace CalendarApp.ViewModels
{
    public class EventEditorViewModel : BaseViewModel
    {
        private readonly CalendarEvent _calendarEvent;
        private string _title;
        private string _description;
        private DateTime _startTime;
        private DateTime _endTime;

        public EventEditorViewModel(CalendarEvent calendarEvent)
        {
            _calendarEvent = calendarEvent;
            _title = _calendarEvent.Title;
            _description = _calendarEvent.Description;
            _startTime = _calendarEvent.StartTime;
            _endTime = _calendarEvent.EndTime;

            SaveCommand = new AppCommand(SaveChanges);
            CancelCommand = new AppCommand(Cancel);
        }

        public event EventHandler<CloseEditorRequest> CloseWindow;

        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public string Description { get => _description; set => SetProperty(ref _description, value); }

        public DateTime StartTime { get => _startTime; set => SetProperty(ref _startTime, value); }

        public DateTime EndTime { get => _endTime; set => SetProperty(ref _endTime, value); }

        public bool AnyUnsavedData => IsUnsavedData();

        public AppCommand SaveCommand { get; }

        public AppCommand CancelCommand { get; }

        internal void SaveChanges()
        {
            _calendarEvent.Title = _title;
            _calendarEvent.Description = _description;
            _calendarEvent.StartTime = _startTime;
            _calendarEvent.EndTime = _endTime;

            CloseWindow.Invoke(this, new CloseEditorRequest { DataSaved = true, Event = _calendarEvent });
        }

        protected override bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            var result = base.SetProperty(ref field, value, propertyName);
            if (result)
                OnPropertyChanged(nameof(AnyUnsavedData));

            return result;
        }

        private bool IsUnsavedData()
        {
            return _title != _calendarEvent.Title ||
                   _description != _calendarEvent.Description ||
                   _startTime != _calendarEvent.StartTime ||
                   _endTime != _calendarEvent.EndTime;
        }

        // todo : add a check for unsaved data and prompt user to save changes before closing the window
        private void Cancel() => CloseWindow.Invoke(this, new CloseEditorRequest { DataSaved = false, Event = _calendarEvent });
    }
}