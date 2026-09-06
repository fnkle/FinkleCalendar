using CalendarApp.Models;
using CalendarApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CalendarApp.ViewModels
{
    public class EventViewModel : BaseViewModel
    {
        private CalendarEvent _calendarEvent;
        private string _title;
        private string _description;
        private DateTime _startTime;
        private DateTime _endTime;

        public EventViewModel(CalendarEvent calendarEvent)
        {
            _calendarEvent = calendarEvent;
            _title = _calendarEvent.Title;
            _description = _calendarEvent.Description;
            _startTime = _calendarEvent.StartTime;
            _endTime = _calendarEvent.EndTime;
        }

        public string Title
        {
            get => _title;

            set
            {
                if (SetProperty(ref _title, value))
                    _calendarEvent.Title = value;
            }
        }

        public string Description
        {
            get => _description;

            set
            {
                if (SetProperty(ref _description, value))
                    _calendarEvent.Description = value;
            }
        }

        public DateTime StartTime
        {
            get => _startTime;

            set
            {
                // Temporary as need to update model before events
                // need a better was for this in future
                _calendarEvent.StartTime = value;
                SetProperty(ref _startTime, value);
            }
        }

        public DateTime EndTime
        {
            get => _endTime;

            set
            {
                // Temporary as need to update model before events
                // need a better was for this in future
                _calendarEvent.EndTime = value;
                SetProperty(ref _endTime, value);
            }
        }

        public CalendarEvent CalendarEvent { get => _calendarEvent; }

        public void CalendarEventClicked(EventViewModel sender, MouseButtonEventArgs e)
        {
            var view = new EventEditorView(new EventEditorViewModel(sender));
            view.Show();
        }
    }
}