using AutoMapper;
using Demo.BusinessLogic.DataTransferObject.EmployeeDTO;
using Demo.DataAccess.model.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDTo>()
                  .ForMember(dst => dst.EmployeeType, Options => Options.MapFrom(src => src.EmType))
                  .ForMember(dst => dst.Gender, Options => Options.MapFrom(src => src.EmpGender));



            CreateMap<Employee, EmployeeDetailsDTO>()
                .ForMember(dst => dst.EmployeeType, Options => Options.MapFrom(src => src.EmType))
                 .ForMember(dst => dst.Gender, Options => Options.MapFrom(src => src.EmpGender))
                 .ForMember(dst => dst.HireDate, Options => Options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                 .ForMember(dst => dst.Image, Options => Options.MapFrom(src => src.ImageName));





            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dst => dst.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));




            CreateMap<UpdateEmploteeDTO, Employee>()
                .ForMember(dst => dst.HiringDate, Options => Options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }
    }
}
