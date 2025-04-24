using Demo.BusinessLogic.DataTransferObject.EmployeeDTO;
using Demo.DataAccess.model.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Interfaces
{
   public  interface IEmployeeService
    {
        IEnumerable<EmployeeDTo> GetAllEmployees(string? EmployeeSearchName, bool withTraking=false );

        EmployeeDetailsDTO? GetEmployeeById(int id);
        int createEmployee(CreatedEmployeeDto createdEmployeeDto);
    
        int updateEmployee(UpdateEmploteeDTO updateEmploteeDTO);
        bool deleteEmployee(int id);

    
    
    } 
}
