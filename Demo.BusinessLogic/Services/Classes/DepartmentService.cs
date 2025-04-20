using Demo.BusinessLogic.DataTransferObject;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Repositories.Class;
using Demo.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    //by using Primary constructor
    {


        //Get all departments
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();

            return departments.Select(d => d.ToDepartmentDto());
        }


        //Get By Id
        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            ///we ca make mapping by many methods
            //1-Manual mapping
            //2-Auto mapper 
            //3-Constructor mapping 
            //4-Exstention mapping 

            return department is null ? null : department.ToDepartmentDetailsDto();

        }


        //Create new department 

        public int AddDepartment(CreateDepartmentDto departmentDto)
        {

            var department = departmentDto.toEntity();
            return _departmentRepository.Add(department);

        }


        //Upddate Department 
        public int UpdateDepartment(UpdateDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.toEntity());
        }



        //Delete Department 
        public bool DelateDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is null)
            {
                return false;
            }
            else
            {
                int result = _departmentRepository.Remove(Department);
                return result > 0 ? true : false;
            }
        }

     
    }
}
