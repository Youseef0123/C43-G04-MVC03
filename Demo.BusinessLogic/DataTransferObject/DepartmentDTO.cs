using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObject
{
    public  class DepartmentDTO
    {
        public int DepId { get; set; }
        public string Name { get; set; }
        public string  Code { get; set; }=string.Empty;
        public string  Description { get; set; }= string.Empty;
        public DateOnly? DateofCreation { get; set; }

    }
}
