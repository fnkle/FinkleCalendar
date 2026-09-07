using CalendarApp.Events;
using CalendarApp.Models;
using CalendarApp.ViewModels;

namespace CalendarApp.Utilies
{
    public interface IWindowService
    {
        event EventHandler<EventUpdatedEventArgs> EventUpdated;

        public void RequestEditWindow(Guid eventId);
    }
}