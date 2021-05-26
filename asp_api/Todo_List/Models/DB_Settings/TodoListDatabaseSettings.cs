using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo_List.Models.DB_Settings
{
    public class TodoListDatabaseSettings : MyTodoListDatabaseSettings
    {
        public string ItemsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface MyTodoListDatabaseSettings
    {
        string ItemsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
