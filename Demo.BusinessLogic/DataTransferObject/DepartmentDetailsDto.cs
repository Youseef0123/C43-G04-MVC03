using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObject
{
    public  class DepartmentDetailsDto
    {

        public int Id { get; set; } //PK
        public int CreatedBy { get; set; } //User Id
        public DateOnly CreatedOn { get; set; }

        public int LastModifyBy { get; set; } //User Id
        public DateOnly LastModifyOn { get; set; }
        public bool IsDeleted { get; set; }  //Soft delete

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly DateOfCreation { get; set; }



    }
}
