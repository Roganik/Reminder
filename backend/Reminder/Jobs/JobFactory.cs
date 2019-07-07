using System;
using Reminder.Interfaces;
using Reminder.MessageHandling;

namespace Reminder.Jobs
{
    public class JobFactory
    {
        private MessageParser _parser;

        public JobFactory()
        {
            _parser = new MessageParser();
        }

        public IJob GetJob(int? userID, string messageText)
        {
            if (userID == null)
            {
                return new WelcomeJob();
            }

            var userTimeInfo = new UserTime() //todo: get by userID
            {
                CurrentUserTime = DateTime.Now
            };

            var parsedMessage = _parser.Parse(messageText, userTimeInfo);
            if (parsedMessage != null)
            {
                return new ScheduleReminderJob();
            }

            throw new NotImplementedException("Cannot process message");
        }
    }
}