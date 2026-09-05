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
			}).Build();
		}

		public static IHost AppHost { get; private set; }
	}
}