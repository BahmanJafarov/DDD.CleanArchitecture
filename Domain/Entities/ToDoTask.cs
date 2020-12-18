using AppDomain.Common.Entities;
using AppDomain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppDomain.Entities
{
    public class ToDoTask : BaseEntity<int>, ICreationAudited, IModificationAudited, IDeletionAudited, ISoftDelete
    {
        public string Name { get; set; }

        public bool IsCompleted { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskState State { get; set; }


        public bool IsDeleted { get; set; }

        public long? DeletedUserId { get; set; }

        public DateTime? DeletedDate { get; set; }


        public long? LastModifiedUserId { get; set; }

        public DateTime? LastModifiedDate { get; set; }


        public long? CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; }


        // navigation properties
        public int AssignedPersonId { get; set; }
        public Person Person { get; set; }
    }
}
