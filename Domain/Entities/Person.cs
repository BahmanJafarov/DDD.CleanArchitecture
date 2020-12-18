﻿using AppDomain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppDomain.Entities
{
    public class Person : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public short Age { get; set; }

        public ICollection<ToDoTask> Tasks { get; set; }
    }
}
