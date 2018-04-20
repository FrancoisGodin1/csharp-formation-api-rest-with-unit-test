using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Dal.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(ulong id)
            : base ($"la task {id} n'existe pas")
        {

        }
    }
}
