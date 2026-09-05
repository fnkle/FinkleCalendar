using CalendarApp.Models;
using CalendarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalendarApp.ViewModels
{
	public class DayViewModel : BaseViewModel
	{
		private Day _day;

		public DayViewModel(Day day)
		{
			_day = day;
			CalendarEvents = new ObservableCollection<CalendarEvent>(day.Events);
		}

		public ObservableCollection<CalendarEvent> CalendarEvents { get; set; }

		public string DayNumber => _day.DayNumber;

		public void CalendarEventClicked(object sender, MouseButtonEventArgs e)
		{
			var calendarEvent = ((FrameworkElement)sender).DataContext as CalendarEvent;
			new EventEditorView(new EventEditorViewModel(calendarEvent)).Show();
		}
	}
}