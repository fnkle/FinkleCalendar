using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.ViewModels
{
    public class EventEditorViewModel : BaseViewModel
    {
        private EventViewModel _eventViewModel;

        private string _title;
        private string _description;
        private DateTime _startTime;
        private DateTime _endTime;

        private bool _unsavedData;

        public EventEditorViewModel(EventViewModel vm)
        {
            _eventViewModel = vm;
            _title = _eventViewModel.Title;
            _description = _eventViewModel.Description;
            _startTime = _eventViewModel.StartTime;
            _endTime = _eventViewModel.EndTime;
        }

        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public DateTime StartTime { get => _startTime; set => SetProperty(ref _startTime, value); }
        public DateTime EndTime { get => _endTime; set => SetProperty(ref _endTime, value); }

        // Only update the eventviewmodel on close which goes through to model
        internal void SaveChanges()
        {
            _eventViewModel.Title = _title;
            _eventViewModel.Description = _description;
            _eventViewModel.StartTime = _startTime;
            _eventViewModel.EndTime = _endTime;
        }
    }
}