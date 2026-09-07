using CalendarApp.Models;
using CalendarApp.Utilies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Persistence
{
    public class CalendarEventPersister : ICalendarEventPersister
    {
        public void SaveEvents(List<CalendarEvent> events)
        {
            var json = JsonConvert.SerializeObject(events, Formatting.Indented);
            var tempDirectory = Path.Combine(Path.GetTempPath(), "CalendarApp");

            if (!Directory.Exists(tempDirectory))
                Directory.CreateDirectory(tempDirectory);

            var stream = File.Create(Path.Combine(tempDirectory, "events.json"));
            stream.Write(Encoding.UTF8.GetBytes(json));
            stream.Close();
        }

        public List<CalendarEvent> LoadEvents()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), "CalendarApp");

            if (Directory.Exists(tempDirectory))
            {
                var filePath = Path.Combine(tempDirectory, "events.json");
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var events = JsonConvert.DeserializeObject<List<CalendarEvent>>(json);
                    if (events != null)
                    {
                        return events;
                    }
                }
            }
            return new List<CalendarEvent>();
        }
    }
}