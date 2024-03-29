﻿using AppDomain.Common.Exceptions;
using AppDomain.Common.Interfaces;
using AppDomain.Entities;
using AppDomain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Services
{
    public class TaskManager : ITaskManager
    {
        public const int MaxActiveTaskCountForAPerson = 3;

        private readonly IRepository<ToDoTask, int> _taskRepository;


        public TaskManager(IRepository<ToDoTask, int> taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task AssignTaskToPerson(ToDoTask task, Person person)
        {
            if (task.AssignedPersonId == person.Id)
            {
                return;
            }

            if (task.State != TaskState.Active)
            {
                throw new ApplicationException("Can not assign a task to a person when task is not active!");
            }

            if (await HasPersonMaximumAssignedTasks(person))
            {
                throw new UserFriendlyException($"{person.Name} already have at most {MaxActiveTaskCountForAPerson} active tasks");
            }

            task.AssignedPersonId = person.Id;
        }


        private async Task<bool> HasPersonMaximumAssignedTasks(Person person)
        {
            var assignedTaskCount = await _taskRepository.Count(t => t.State == TaskState.Active && t.AssignedPersonId == person.Id);
            return assignedTaskCount >= MaxActiveTaskCountForAPerson;
        }
    }
}
