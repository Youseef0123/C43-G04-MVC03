using Demo.DataAccess.model.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.model.EmployeeModel
{
    public  class Employee:BaseEntity
    {
        public  string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gendr EmpGender { get; set; }
        public EmployeeType EmType { get; set; }


    }
}
