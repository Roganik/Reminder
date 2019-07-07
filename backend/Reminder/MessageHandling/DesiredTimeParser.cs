using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Reminder.MessageHandling
{
    public class DesiredTimeParser
    {
        private UserTime _userTime { get; set; }

        public DesiredTimeParser(UserTime userTime)
        {
            _userTime = userTime;
        }

        public DateTime GetDesiredNotificationUserTime(string text)
        {
            var time = ParseExplicitMinutesAndHours(text);
            var timeOffset = ParseTimeOffset(text);
            var timeOffsetDays = ParseTimeOffsetDays(text);

            var desiredTime = _userTime.CurrentUserTime;
            if (time.isParsed)
            {
                desiredTime = new DateTime(desiredTime.Year, desiredTime.Month, desiredTime.Day, time.hours, time.minutes, 0);
                if (desiredTime < _userTime.CurrentUserTime)
                {
                    desiredTime = desiredTime.AddDays(1);
                }
            }

            if (timeOffset.isParsed)
            {
                desiredTime = desiredTime.AddDays(timeOffset.addDays);
                desiredTime = desiredTime.AddHours(timeOffset.addHours);
                desiredTime = desiredTime.AddMinutes(timeOffset.addMinutes);
            }

            if (timeOffsetDays.isParsed)
            {
                desiredTime = desiredTime.AddDays(timeOffsetDays.addDays);
            }

            return desiredTime; //todo: return matched strings and separate  matched text from remider text
        }

        private (bool isParsed, string matchText, int hours, int minutes) ParseExplicitMinutesAndHours(string text)
        {
            var certainTimeParser = new Regex(@"(\d{1,2})[:-](\d{1,2})");
            if (!certainTimeParser.IsMatch(text))
            {
                return (false, null, 0, 0);
            }
            var match = certainTimeParser.Match(text);
            int.TryParse(match.Groups[1].Value, out var hours);
            int.TryParse(match.Groups[2].Value, out var minutes);

            return (true, match.Value, hours, minutes);
        }

        private (bool isParsed, string matchText, int addDays) ParseTimeOffsetDays(string text)
        {
            if (text.Contains("после завтра"))
            {
                return (true, "после завтра", 2);
            }

            if (text.Contains("послезавтра"))
            {
                return (true, "послезавтра", 2);
            }

            if (text.Contains("завтра"))
            {
                return (true, "завтра", 1);
            }

            return (false, null, 0);
        }

        private (bool isParsed, string[] matchText, int addMinutes, int addHours, int addDays) ParseTimeOffset(string text)
        {
            var matchtexts = new string[] {};

            var minuteParser = new Regex(@"через( \d+|) (минуту|минуты|минут)");
            var hourParser = new Regex(@"через( \d+|) (час|часа|часов)");
            var dayParser = new Regex(@"через( \d+|) (дня|дней|день)");

            //todo: handle "полчаса", "полтора часа"
            //todo: hande weeks, months and years

            int ReadFromRegex(Regex reg)
            {
                if (!reg.IsMatch(text))
                {
                    return 0;
                }

                var match = reg.Match(text);
                matchtexts.Append(match.Value);

                return string.IsNullOrEmpty(match.Groups[1].Value)
                    ? 1 //empty first group might be with text like 'через минуту'
                    : int.Parse(match.Groups[1].Value);

            }

            var addMinutes = ReadFromRegex(minuteParser);
            var addHours = ReadFromRegex(hourParser);
            var addDays = ReadFromRegex(dayParser);

            var isParsed = addMinutes != 0 || addHours != 0 || addDays != 0;
            return (isParsed, matchtexts, addMinutes, addHours, addDays);
        }
    }
}