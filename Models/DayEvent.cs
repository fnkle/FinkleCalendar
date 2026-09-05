using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CalendarApp.Models
{
	internal class DayEvent
	{
		private string _description;
		private DateTime _endTime;
		private Guid _id;
		private DateTime _startTime;
		private string _title;

		public Guid Id => _id;
		public string Title => _title;
		public string Description => _description;
		public DateTime StartTime => _startTime;
		public DateTime EndTime => _endTime;
		public TimeSpan Duration => _endTime.Subtract(_startTime);
	}
}