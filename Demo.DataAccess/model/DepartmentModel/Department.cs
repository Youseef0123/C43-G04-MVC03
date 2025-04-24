﻿using Demo.DataAccess.model.EmployeeModel;
using Demo.DataAccess.model.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.model
{
    public  class Department:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string code { get; set; } = null!;
        public string? Description { get; set; }

        public  ICollection<Employee>  Employees { get; set; }   //Navigation Property

    }
}
