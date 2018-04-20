using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kanban.Dal
{
    public class DapperTaskRepository : ITaskRepository
    {
        private string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\projetsFG\formationApiRest\jour2\Kanban.Web\Kanban.Web\App_Data\KanbanDb.mdf;Integrated Security = True";
        public List<Task> GetAll()
        {
            DbConnection db = new SqlConnection(connectionString);
            return db
                .Query<Task>("select * from Task")
                .ToList();
        }

        public void Add(Task task)
        {
            throw new NotImplementedException();
        }

        public void Delete(ulong id)
        {
            throw new NotImplementedException();
        }

        public void Update(Task task)
        {
            throw new NotImplementedException();
        }
    }
}
