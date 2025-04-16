using Demo.DataAccess.model.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interface
{
    public interface IEmployeeRepo
    {
        int Add(Employee Employee);
        IEnumerable<Employee> GetAll(bool withTracking = false);
        Employee? GetById(int id);
        int Remove(Employee Employee);
        int Update(Employee Employee); 
    }
}
