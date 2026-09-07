using CalendarApp.Utilies;
using CalendarApp.ViewModels;
using Core.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace CalendarApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                services.AddSingleton<ICalendarEventRepository, CalendarEventRepository>();
                services.AddSingleton<IWindowService, WindowService>();
                services.AddSingleton<ICalendarEventPersister, CalendarEventPersister>();

                services.AddSingleton<MainWindowViewModel>();

                services.AddTransient<MainWindow>();
            }).Build();
        }

        public static IHost AppHost { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (AppHost)
            {
                var persistentDataService = AppHost.Services.GetRequiredService<ICalendarEventPersister>();
                var eventRepository = AppHost.Services.GetRequiredService<ICalendarEventRepository>();
                persistentDataService.SaveEvents(eventRepository.GetAllEvents());
                await AppHost!.StopAsync();
            }

            base.OnExit(e);
        }
    }
}