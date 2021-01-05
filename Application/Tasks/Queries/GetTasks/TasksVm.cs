using AppDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Tasks.Queries.GetTasksQuery
{
    public class TasksVm
    {
        public IList<EnumValueDto> TaskPriorities { get; } =
            Enum.GetValues(typeof(TaskPriority))
                .Cast<TaskPriority>()
                .Select(p => new EnumValueDto { Value = (int)p, Name = p.ToString() })
                .ToList();

        public IList<EnumValueDto> TaskStates =
            Enum.GetValues(typeof(TaskState))
                .Cast<TaskState>()
                .Select(s => new EnumValueDto { Value = (int)s, Name = s.ToString() })
                .ToList();

        public IList<TaskDto> Tasks { get; set; }
    }
}
