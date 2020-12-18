using AppDomain.Common.DomainEvents;
using AppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppDomain.Events
{
    public class TaskCompletedEvent : BaseDomainEvent
    {
        public ToDoTask CompletedTask { get; set; }

        public TaskCompletedEvent(ToDoTask completedTask)
        {
            CompletedTask = completedTask;
        }
    }
}
