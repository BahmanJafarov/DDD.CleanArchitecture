using AppDomain.Entities;
using AppDomain.Enums;
using Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Queries.GetTasksQuery
{
    public class TaskDto : IMapFrom<ToDoTask>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskState State { get; set; }
    }
}
