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
                var eventVm = new EventViewModel(calendarEvent);
                eventVm.PropertyChanged += OnEventUpdated;
                CalendarEvents.Add(eventVm);
            }
        }

        public event EventHandler<EventViewModel, string> EventChanged;

        public ObservableCollection<EventViewModel> CalendarEvents { get; set; }

        public string DayNumber => _day.DayNumber.ToString();

        public Day Day => _day;

        private void OnEventUpdated(object? sender, PropertyChangedEventArgs e)
        {
            if (!(sender is EventViewModel eventVm))
                return;

            if (e.PropertyName == nameof(EventViewModel.StartTime) || e.PropertyName == nameof(EventViewModel.EndTime))
            {
                EventChanged.Invoke(eventVm, e.PropertyName);
            }
        }
    }
}