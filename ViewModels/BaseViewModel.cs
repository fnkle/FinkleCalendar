using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CalendarApp
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string? name = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? name = null)
		{
			if (field != null &&
				field.Equals(value))
			{
				return false;
			}

			field = value;
			OnPropertyChanged(name);
			return true;
		}
	}
}