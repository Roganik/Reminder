using System.Threading.Tasks;

namespace Reminder.Interfaces
{
    public interface IJob
    {
        Task Execute();
    }
}