using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace CalendarApp
{
	public class MainWindowViewModel : BaseViewModel
	{
		public ObservableCollection<Cell> Cells { get; } = new ObservableCollection<Cell>();
		public String CurrentMonthYear { get => _currentMonthYear ; set => SetProperty(ref _currentMonthYear, _currentDate.ToString("MMMM yyyy")); }

		public AppCommand NextMonthCommand => new AppCommand(NextMonth);

		public AppCommand PrevMonthCommand => new AppCommand(PrevMonth);

		public int ColumnCount { get; }

		private DateTime _currentDate = DateTime.Now;

		private string _currentMonthYear;

		public MainWindowViewModel()
		{
			Update();
		}

		public class Cell
		{
			public Cell(int value) { Value = value.ToString(); }
			public string Value { get; set; }
		}
		private void NextMonth()
		{
			_currentDate = _currentDate.AddMonths(1);
			Update();
		}

		private void PrevMonth()
		{
			_currentDate = _currentDate.AddMonths(-1);
			Update();
		}

		private void Update()
		{
			Cells.Clear();
			var numDays = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

			for (int i = 0; i < numDays; i++)
			{
				Cells.Add(new Cell(i + 1));
			}

			CurrentMonthYear = _currentDate.ToString("MMMM yyyy");
		}
	}
}
