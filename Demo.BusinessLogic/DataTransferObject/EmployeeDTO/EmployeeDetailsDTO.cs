using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObject.EmployeeDTO
{
    public class EmployeeDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        public int? Age { get; set; }
        public string? Addrees { get; set; }
        public decimal Salary { get; set; }
        public bool  IsActive { get; set; }
        public string  Email { get; set; }
        public string  PhoneNumber { get; set; }
        public DateOnly HireDate { get; set; }
        public string Gender { get; set; }
        public string  EmployeeType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; } = null;
        public int  LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; } = null;

        public string? Image { get; set; }




    }
}
