namespace CalendarApp.Models
{
    public class CalendarEvent
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

        public Guid Id => _id;
        public string Title { get => _title; set => _title = value; }
        public string Description { get => _description; set => _description = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }
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

        public bool OnDay(Day day)
        {
            return OnDay(day.DayNumber, day.MonthNumber, day.YearNumber);
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
    }
}