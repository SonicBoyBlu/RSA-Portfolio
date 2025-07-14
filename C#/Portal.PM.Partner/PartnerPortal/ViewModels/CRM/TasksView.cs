using System.Collections.Generic;

namespace Acme.ViewModels.CRM
{
    public class TasksView
    {
        public TasksView()
        {
            Tasks = new List<Models.CRM.Tasks.Task>();
        }
        public Models.CRM.Search Query { get; set; }
        public List<Models.CRM.Tasks.Task> Tasks { get; set; }
    }
}