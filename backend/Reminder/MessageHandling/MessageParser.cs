using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Reminder.MessageHandling
{
    public class MessageParser
    {
        public ParsedMessage Parse(string text, UserTime userTime)
        {
            var originalText = text;
            text = NormalizeText(text);

            var timeHelper = new DesiredTimeParser(userTime);
            var userReminderTime = timeHelper.GetDesiredNotificationUserTime(text);

            var result = new ParsedMessage()
            {
                ReminderText = originalText,
                UserReminderTime = userReminderTime,
            };

            return result;
        }

        private string NormalizeText(string text)
        {
            text = text.ToLowerInvariant();
            //todo: remove useless words (like 'напомни')

            var digitMapping = new Dictionary<Regex, int>()
            {
                { new Regex(@"(один|первого)"), 1 },
                { new Regex(@"(два|второго)"), 2 },
                { new Regex(@"(три|третьего)"), 3 },
                { new Regex(@"(четыре|четвертого)"), 4 },
                { new Regex(@"(пять|пятого)"), 5 },
                { new Regex(@"(шесть|шестого)"), 6 },
                { new Regex(@"(семь|седьмого)"), 7 },
                { new Regex(@"(восемь|восьмого)"), 8 },
                { new Regex(@"(двевять|девятого)"), 9 },
                { new Regex(@"(десять|десятого)"), 10 },
                //todo: look for russian words2digits parser
            };

            foreach (var regex in digitMapping.Keys)
            {
                if (regex.IsMatch(text))
                {
                    var match = regex.Match(text);
                    text = text.Replace(match.Value, digitMapping[regex].ToString());
                }
            }

            return text;
        }
    }
}