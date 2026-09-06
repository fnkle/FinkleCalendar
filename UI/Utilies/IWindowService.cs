using CalendarApp.Models;
using CalendarApp.ViewModels;

namespace CalendarApp.Utilies
{
    public interface IWindowService
    {
        void CloseEditorWindow(EventViewModel eventViewModel);

        void ShowEditorWindow(EventViewModel calendarEvent);
    }
}
