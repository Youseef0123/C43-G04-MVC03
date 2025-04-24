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
    public class DepartmentService(IUnitOfWork _unitOfWork ) : IDepartmentService
    //by using Primary constructor
    {


        //Get all departments
        public IEnumerable<DepartmentDTO> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return departments.Select(d => d.ToDepartmentDto());


        }


        //Get By Id
        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
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
             _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.SaveChangs();


        }


        //Upddate Department 
        public int UpdateDepartment(UpdateDto departmentDto)
        {
             _unitOfWork.DepartmentRepository.Update(departmentDto.toEntity());
            return _unitOfWork.SaveChangs();    

        }



        //Delete Department 
        public bool DelateDepartment(int id)
        {
            var Department = _unitOfWork.DepartmentRepository.GetById(id);
            if (Department is null)
            {
                return false;
            }
            else
            {
                 _unitOfWork.DepartmentRepository.Remove(Department);
                int result= _unitOfWork.SaveChangs();

                if(result > 0) return true;
                 else 
                    return false;
            }
        }

     
    }
}
