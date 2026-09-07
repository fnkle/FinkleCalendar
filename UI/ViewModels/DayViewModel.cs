using CalendarApp.Events;
using CalendarApp.Models;
using CalendarApp.Utilies;
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
using UI.Events;

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
                var eventVm = new EventViewModel(calendarEvent);
                eventVm.EventClicked += (s, e) => EventEditRequest?.Invoke(this, new EventEditRequestArgs { EventId = e.EventId });
                CalendarEvents.Add(eventVm);
            }
        }

        public event EventHandler<EventUpdatedEventArgs> EventChanged;

        public event EventHandler<EventEditRequestArgs> EventEditRequest;

        public ObservableCollection<EventViewModel> CalendarEvents { get; set; }

        public string DayNumber => _day.DayNumber.ToString();

        public Day Day => _day;
    }
}