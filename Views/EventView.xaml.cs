using CalendarApp.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalendarApp.Views
{
    /// <summary>
    /// Interaction logic for EventView.xaml
    /// </summary>
    public partial class EventView : UserControl
    {
        public EventView()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) => ((EventViewModel)DataContext).CalendarEventClicked((EventViewModel)DataContext, e);
    }
}