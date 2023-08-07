using ASPCORE.Infrastructure;

namespace ASPCORE.Repository
{
    public class TaskService : ITransientService, IScopedService, ISingletonService
    {
        readonly Guid id;
        public TaskService()
        {
            id = Guid.NewGuid();
        }
        public Guid GetTaskID()
        {
            return id;//Here we generating our Guid id using different Interfaces
        }
    }
}
