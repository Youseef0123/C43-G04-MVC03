using AutoMapper;
using Demo.BusinessLogic.DataTransferObject.EmployeeDTO;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.model.EmployeeModel;
using Demo.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepo _employeeRepo, IMapper _mapper) : IEmployeeService
    {
        public int createEmployee(CreatedEmployeeDto createdEmployeeDto)
        {
           var employee = _mapper.Map<CreatedEmployeeDto,Employee>(createdEmployeeDto);
           return _employeeRepo.Add(employee);

        }

        public bool deleteEmployee(int id)
        {
            var employee = _employeeRepo.GetById(id);
            if (employee is null) return false;

            employee.IsDeleted = true;
            return _employeeRepo.Update(employee) > 0;
        }

        public IEnumerable<EmployeeDTo> GetAllEmployees(bool withTraking = false  )
        {
            var employee = _employeeRepo.GetAll(withTraking);
            //Auto Mapper 

            /// Map<Src , Destination>      src is (from)    Destination is (to)
            var employeeDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTo>>(employee);

            //Manual Mapping 
            //var employeeDto = employee.Select(Emp =>new EmployeeDTo()
            //{
            //    Id=Emp.Id,
            //    Name=Emp.Name,
            //    Age=Emp.Age,
            //    IsActive=Emp.IsActive,
            //    Salary=Emp.Salary,
            //    EmployeeType=Emp.EmployeeType.ToString(),
            //    Gender=Emp.EmpGender.ToString(),

            //});

            return employeeDto;
        }

        EmployeeDetailsDTO IEmployeeService.GetEmployeeById(int id)
        {
           var employee =_employeeRepo.GetById(id);
            if (employee == null) return null;
            else
                return  _mapper.Map<EmployeeDetailsDTO>(employee);   ////we can write the Destination onl and he knwo the src from the object will send to
                //    new EmployeeDetailsDTO()
                //{
                //    Id = employee.Id,
                //    Name = employee.Name,
                //    Salary = employee.Salary,
                //    Addrees = employee.Address,
                //    Age = employee.Age,
                //    Email = employee.Email,
                //    IsActive = employee.IsActive,
                //    HireDate = DateOnly.FromDateTime(employee.HiringDate),
                //    PhoneNumber = employee.PhoneNumber,
                //    EmployeeType = employee.EmployeeType.ToString(),
                //    Gender = employee.EmpGender.ToString(),
                //    CreatedBy = 1,
                //    CreatedOn = employee.CreatedOn,
                //    LastModifiedBy = 1,
                //    LastModifiedOn = employee.LastModifyOn,
                //};
        }

        int IEmployeeService.updateEmployee(UpdateEmploteeDTO updateEmploteeDTO)
        {
            return _employeeRepo.Update(_mapper.Map<UpdateEmploteeDTO, Employee>(updateEmploteeDTO));
        }
    }
}
