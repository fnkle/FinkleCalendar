using CalendarApp.Models;
using CalendarApp.ViewModels;

namespace CalendarApp.Utilies
{
    public interface IWindowService
    {
        public void RequestEditWindow(Guid eventId, DateTime dateTime);
    }
}