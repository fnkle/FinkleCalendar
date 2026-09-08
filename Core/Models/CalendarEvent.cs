using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalendarApp.Models
{
    public class CalendarEvent : INotifyPropertyChanged
    {
        private Guid _id;
        private string _title;
        private string _description;
        private DateTime _endTime;

        private DateTime _startTime;

        public CalendarEvent(DateTime startTime, DateTime endTime)
        {
            _id = Guid.NewGuid();
            _startTime = startTime;
            _endTime = endTime;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Guid Id => _id;
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public DateTime StartTime { get => _startTime; set => SetProperty(ref _startTime, value); }
        public DateTime EndTime { get => _endTime; set => SetProperty(ref _endTime, value); }
        public TimeSpan Duration => _endTime.Subtract(_startTime);

        public bool InMonth(int month, int year)
        {
            if (_startTime.Month == month &&
                _startTime.Year == year)
            {
                return true;
            }

            if (_endTime.Month == month &&
                _endTime.Year == year)
            {
                return true;
            }

            var testDay = new DateTime(day: 14, month: month, year: year);
            return testDay.CompareTo(_startTime) > 0 &&
                        testDay.CompareTo(_endTime) < 0;
        }

        public bool OnDay(int day, int month, int year)
        {
            if (_startTime.Day == day &&
                _startTime.Month == month &&
                _startTime.Year == year)
            {
                return true;
            }

            if (_endTime.Day == day &&
                _endTime.Month == month &&
                _endTime.Year == year)
            {
                return true;
            }

            var testDay = new DateTime(day: day, month: month, year: year);
            return testDay.CompareTo(_startTime) > 0 &&
                        testDay.CompareTo(_endTime) < 0;
        }

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}