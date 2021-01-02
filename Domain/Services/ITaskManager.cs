using AppDomain.Entities;
using System.Threading.Tasks;

namespace AppDomain.Services
{
    public interface ITaskManager
    {
        Task AssignTaskToPerson(ToDoTask task, Person person);
    }
}
