using System;

namespace Reminder.MessageHandling
{
    public class ParsedMessage
    {
        private TimeSpan ReminderTime { get; set; }
        private string ReminderText { get; set; }
    } 

    public class MessageParser
    {
        public ParsedMessage Parse(string text)
        {
            throw new NotImplementedException(); 
        }
    }
}