using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CalendarApp.Models
{
	public class CalendarEvent
	{
		private Guid _id;
		private string _title;
		private string _description;
		private DateTime _endTime;
		private DateTime _startTime;

		public CalendarEvent(DateTime startTime, DateTime endTime)
		{
			_id = Guid.NewGuid();
			_startTime = startTime;
			_endTime = endTime;
		}

		public Guid Id => _id;
		public string Title { get => _title; set => _title = value; }
		public string Description { get => _description; set => _description = value; }
		public DateTime StartTime { get => _startTime; set => _startTime = value; }
		public DateTime EndTime { get => _endTime; set => _endTime = value; }
		public TimeSpan Duration => _endTime.Subtract(_startTime);

		public bool InMonth(int month, int year)
		{
			return (_startTime.Month <= month && _endTime.Month >= month) &&
					   (_startTime.Year <= year && _endTime.Year >= year);
		}

		public bool OnDay(int day, int month, int year)
		{
			return (_startTime.Month <= month && _endTime.Month >= month) &&
					   (_startTime.Year <= year && _endTime.Year >= year) &&
					   (_startTime.Day <= day && _endTime.Day >= day);
		}
	}
}