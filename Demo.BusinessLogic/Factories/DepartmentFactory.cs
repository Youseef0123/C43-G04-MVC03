using Demo.BusinessLogic.DataTransferObject;
using Demo.DataAccess.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{

    //we using Extention methods to mapping 
    static class DepartmentFactory
    {
        public static DepartmentDTO ToDepartmentDto(this Department d)
        {
            return new DepartmentDTO()
            {
                DepId = d.Id,
                Code = d.code,
                Description = d.Description,
                Name = d.Name,
                DateofCreation = d.CreatedOn.HasValue ? DateOnly.FromDateTime(d.CreatedOn.Value) : null

            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department d)
        {
            return new DepartmentDetailsDto()
            {
                Id = d.Id,
                Name = d.Name,
                Code=d.code,
                Description=d.Description,
                CreatedOn = DateOnly.FromDateTime(d.CreatedOn ?? default)
            };
        }

        public static Department toEntity(this CreateDepartmentDto D)
        {
            return new Department()
            {

                Name = D.Name,
                code = D.Code,
                Description = D.Description,
                CreatedOn = D.DateOfCreation.ToDateTime(new TimeOnly())

            };
        }

        public static Department toEntity(this UpdateDto D)
        {
            return new Department()
            {
                Id = D.Id,
                Name = D.Name,
                code= D.Code,
                Description = D.Description,
                CreatedOn = D.DateOfCreation.ToDateTime(new TimeOnly())
            };
        }


       
    }
}
