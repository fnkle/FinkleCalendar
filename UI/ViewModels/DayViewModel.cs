using CalendarApp.Models;
using CalendarApp.Utilies;
using CalendarApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.Events;

namespace CalendarApp.ViewModels
{
    public class DayViewModel : BaseViewModel
    {
        private readonly string _dayNumber;

        public DayViewModel(List<CalendarEvent> events, int dayNumber)
        {
            CalendarEvents = new ObservableCollection<EventViewModel>();
            _dayNumber = dayNumber.ToString();

            foreach (var calendarEvent in events)
            {
                var eventVm = new EventViewModel(calendarEvent);
                eventVm.EventClicked += (s, e) => EventEditRequest?.Invoke(this, new EventEditRequestArgs { EventId = e.EventId, Day = int.Parse(_dayNumber) });
                CalendarEvents.Add(eventVm);
            }

            AddEventCommand = new AppCommand(() => EventEditRequest?.Invoke(this, new EventEditRequestArgs { EventId = Guid.NewGuid(), Day = int.Parse(_dayNumber) }));
        }

        public event EventHandler<EventEditRequestArgs> EventEditRequest;

        public AppCommand AddEventCommand { get; set; }
        public ObservableCollection<EventViewModel> CalendarEvents { get; set; }

        public string DayNumber => _dayNumber;
    }
}