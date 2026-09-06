using CalendarApp.Models;
using CalendarApp.Utilies;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.ViewModels
{
    public class EventEditorViewModel : BaseViewModel
    {
        private readonly IWindowService _windowService;
        private EventViewModel _eventViewModel;
        private string _title;
        private string _description;
        private DateTime _startTime;
        private DateTime _endTime;

        private bool _unsavedData;

        public EventEditorViewModel(EventViewModel vm, IWindowService windowService)
        {
            _eventViewModel = vm;
            _windowService = windowService;
            _title = _eventViewModel.Title;
            _description = _eventViewModel.Description;
            _startTime = _eventViewModel.StartTime;
            _endTime = _eventViewModel.EndTime;

            SaveCommand = new AppCommand(SaveChanges);
            CancelCommand = new AppCommand(Cancel);
        }

        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public DateTime StartTime { get => _startTime; set => SetProperty(ref _startTime, value); }
        public DateTime EndTime { get => _endTime; set => SetProperty(ref _endTime, value); }

        public AppCommand SaveCommand { get; }
        public AppCommand CancelCommand { get; }

        internal void SaveChanges()
        {
            _eventViewModel.Title = _title;
            _eventViewModel.Description = _description;
            _eventViewModel.StartTime = _startTime;
            _eventViewModel.EndTime = _endTime;
        }

        // todo : add a check for unsaved data and prompt user to save changes before closing the window
        private void Cancel() => _windowService.CloseEditorWindow(_eventViewModel);
    }
}