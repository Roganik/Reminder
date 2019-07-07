using System;

namespace Reminder.MessageHandling
{
    public class ParsedMessage
    {
        public bool IsParsed => UserReminderTime != null && !string.IsNullOrEmpty(ReminderText);

        public string ReminderText { get; set; }
        public DateTime? UserReminderTime { get; set; }
        public DateTime? UtcReminderTime => UserReminderTime?.ToUniversalTime();
    }
}