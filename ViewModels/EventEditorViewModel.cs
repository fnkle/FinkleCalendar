using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.ViewModels
{
	public class EventEditorViewModel : BaseViewModel
	{
		private CalendarEvent _calendarEvent;

		private string _title;
		private string _description;
		private DateTime _startTime;
		private DateTime _endTime;

		private bool _unsavedData;

		public EventEditorViewModel(CalendarEvent calendarEvent)
		{
			_calendarEvent = calendarEvent;
			_title = _calendarEvent.Title;
			_description = _calendarEvent.Description;
			_startTime = _calendarEvent.StartTime;
			_endTime = _calendarEvent.EndTime;
		}

		public string Title { get => _title; set => SetProperty(ref _title, value); }
		public string Description { get => _description; set => SetProperty(ref _description, value); }
		public DateTime StartTime { get => _startTime; set => SetProperty(ref _startTime, value); }
		public DateTime EndTime { get => _endTime; set => SetProperty(ref _endTime, value); }
	}
}