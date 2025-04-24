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
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper) : IEmployeeService
    {
        public int createEmployee(CreatedEmployeeDto createdEmployeeDto)
        {
           var employee = _mapper.Map<CreatedEmployeeDto,Employee>(createdEmployeeDto);
            _unitOfWork.EmployeeRepo.Add(employee);
            return _unitOfWork.SaveChangs();

        }

        public bool deleteEmployee(int id )
        {
            var employee = _unitOfWork.EmployeeRepo.GetById(id);
            if (employee is null) return false;

            employee.IsDeleted = true;
             _unitOfWork.EmployeeRepo.Update(employee);

            return _unitOfWork.SaveChangs() > 0 ? true:false;
        }

        public IEnumerable<EmployeeDTo> GetAllEmployees(string? EmployeeSearchName, bool withTraking = false  )
        {
            //Search Service logic 

            IEnumerable<Employee> employees;
            if(string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees= _unitOfWork.EmployeeRepo.GetAll();
            else
                employees= _unitOfWork.EmployeeRepo.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));

            //Auto Mapper 

            /// Map<Src , Destination>      src is (from)    Destination is (to)
            var employeeDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTo>>(employees);

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
           var employee = _unitOfWork.EmployeeRepo.GetById(id);
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

            _unitOfWork.EmployeeRepo.Update(_mapper.Map<UpdateEmploteeDTO, Employee>(updateEmploteeDTO));

            return _unitOfWork.SaveChangs();
        
        }
    }
}
