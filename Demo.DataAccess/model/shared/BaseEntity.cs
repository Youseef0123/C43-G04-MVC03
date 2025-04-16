using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.model.shared
{
    public class BaseEntity
    {
        public int Id { get; set; } //PK
        public int CreatedBy { get; set; } //User Id
        public DateTime? CreatedOn { get; set; }

        public int LastModifyBy { get; set; } //User Id
        public DateTime? LastModifyOn { get; set; }
        public bool IsDeleted { get; set; }  //Soft delete
    }
}
