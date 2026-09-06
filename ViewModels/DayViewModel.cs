using CalendarApp.Models;
using CalendarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
            CalendarEvents = new ObservableCollection<EventViewModel>();

            foreach (var calendarEvent in _day.Events)
            {
                CalendarEvents.Add(new EventViewModel(calendarEvent));
            }
        }

        public ObservableCollection<EventViewModel> CalendarEvents { get; set; }

        public string DayNumber => _day.DayNumber;
    }
}