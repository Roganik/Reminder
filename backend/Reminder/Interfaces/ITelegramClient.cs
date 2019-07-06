using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace Reminder.Interfaces
{
    public interface ITelegramClient
    {
        Task SendMessage(string text, int chatId);
        void AddMessageHandler(Action<MessageEventArgs> messageHandler);
    }
}