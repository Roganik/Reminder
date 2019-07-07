using System;
using NodaTime;

namespace Reminder.MessageHandling
{
    public class UserTime
    {
        private static string NskTimeZoneName = "Asia/Novosibirsk";// https://en.wikipedia.org/wiki/List_of_tz_database_time_zones
        public DateTimeZone TimeZoneInfo => DateTimeZoneProviders.Tzdb[UserTime.NskTimeZoneName];
        public DateTime CurrentUserTime { get; set; }
    }
}