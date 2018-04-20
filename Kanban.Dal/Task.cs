using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Dal
{
    public enum StatusTask
    {
        TODO,
        ONGOING,
        DONE
    }
    public class Task
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public StatusTask status { get; set; }
        public DateTime create { get; set; }
        public DateTime done { get; set; }


    }
}
