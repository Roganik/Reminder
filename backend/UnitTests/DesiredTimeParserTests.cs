using System;
using System.Collections;
using NUnit.Framework;
using Reminder.MessageHandling;

namespace UnitTests
{
    public class Tests
    {
        private DesiredTimeParser Parser { get; set; }
        private UserTime UserTime { get; set; }

        [SetUp]
        public void Setup()
        {
            UserTime = new UserTime()
            {
                CurrentUserTime = new DateTime(2019, 07, 01, 12, 00, 00),
            };
            Parser = new DesiredTimeParser(UserTime);
        }

        [Test]
        public void ParseClosestTime_ShouldUseToday()
        {
            var parsedTime = Parser.GetDesiredNotificationUserTime("напомни в 18:30 написать сообщение Юре");

            Assert.AreEqual(18, parsedTime.Hour);
            Assert.AreEqual(30, parsedTime.Minute);
            Assert.AreEqual(UserTime.CurrentUserTime.Day, parsedTime.Day);
            Assert.AreEqual(UserTime.CurrentUserTime.Month, parsedTime.Month);
            Assert.AreEqual(UserTime.CurrentUserTime.Year, parsedTime.Year);
        }

        [Test]
        public void ParseClosestTime_ShouldUseTomorrow()
        {
            UserTime.CurrentUserTime = new DateTime(2019, 07, 01, 20, 00, 00);
            var parsedTime = Parser.GetDesiredNotificationUserTime("напомни в 18:30 написать сообщение Юре");

            Assert.AreEqual(18, parsedTime.Hour);
            Assert.AreEqual(30, parsedTime.Minute);
            Assert.AreEqual(UserTime.CurrentUserTime.Day + 1, parsedTime.Day);
            Assert.AreEqual(UserTime.CurrentUserTime.Month, parsedTime.Month);
            Assert.AreEqual(UserTime.CurrentUserTime.Year, parsedTime.Year);
        }

        [Test]
        public void ParseDelayedTime_ShouldAddMinutes()
        {
            var parsedTime = Parser.GetDesiredNotificationUserTime("напомни включить пылесос через 10 минут");

            Assert.AreEqual(UserTime.CurrentUserTime.Hour, parsedTime.Hour);
            Assert.AreEqual(UserTime.CurrentUserTime.Minute + 10, parsedTime.Minute);
            Assert.AreEqual(UserTime.CurrentUserTime.Day, parsedTime.Day);
            Assert.AreEqual(UserTime.CurrentUserTime.Month, parsedTime.Month);
            Assert.AreEqual(UserTime.CurrentUserTime.Year, parsedTime.Year);
        }

        //todo: add test-cases
        //"Напомни в субботу написать отзыв на отель"
        //"напомни через 2 часа 30 минут ответить в слэке"
        //"напомни первого июня про фотосессию с животными"

        //"напомни завтра в 16-00 про кино"
        //"напомни послезавтра в 12-00 про кино"
        //"Напомни через 80 минут позвонить Юре"
        //"Напомни настроить мониторинг скидок в субботу в 14:05"
        //"Напомни через 11 часов забрать игру в EGS"
        //"напомни через 2 часа повернуть манго"
        //"напомни в 7:20 скинуть на флешку"
    }
}