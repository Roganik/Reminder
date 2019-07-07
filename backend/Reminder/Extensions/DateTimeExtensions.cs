using System;

namespace Reminder.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime UtcToLocal(this DateTime source, TimeZoneInfo localTimeZone)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(source, localTimeZone);
        }

        public static DateTime LocalToUtc(this DateTime source, TimeZoneInfo localTimeZone)
        {
            source = DateTime.SpecifyKind(source, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(source, localTimeZone);
        }
    }
}