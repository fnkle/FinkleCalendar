using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CalendarApp.Views
{
    /// <summary>
    /// Interaction logic for EventEditorView.xaml
    /// </summary>
    public partial class EventEditorView : Window
    {
        public EventEditorView(EventEditorViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}