using Kanban.Dal.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Dal
{
    public interface ITaskRepository
    {
        List<Task> GetAll();
        void Add(Task task);
        void Update(Task task);
        void Delete(ulong id);
    }
    public class InMemoryTaskRepository : ITaskRepository
    {
        private static List<Task> tasks = new List<Task>();

        public void Add(Task task)
        {
            ulong id = 0;
            if (tasks.Any())
                id = tasks.Max(t => t.id) + 1;
            task.id = id;
            tasks.Add(task);
        }

        public List<Task> GetAll()
        {
            return tasks;
        }

        //public Task getTaskById(ulong id)
        //{
        //    return tasks.Find(t => t.id == id);
        //}

        public void Update(Task task)
        {
            var taskToUpdate = tasks.SingleOrDefault(t => t.id == task.id);
            if(taskToUpdate == null)
                throw new TaskNotFoundException(task.id);
            
            taskToUpdate.description = task.description;
            taskToUpdate.name = task.name;
            taskToUpdate.status = task.status;
            
 
        }

        public void Delete(ulong id)
        {
            Task deletedTask = tasks.SingleOrDefault(t => t.id == id);
            if (deletedTask == null)
                throw new TaskNotFoundException(id);
            tasks.Remove(deletedTask);
        }
    }
}
