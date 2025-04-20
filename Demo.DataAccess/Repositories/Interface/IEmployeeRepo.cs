using Demo.DataAccess.model.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interface
{
    public interface IEmployeeRepo:IGenericRepository<Employee>
    {
        
    }
}
