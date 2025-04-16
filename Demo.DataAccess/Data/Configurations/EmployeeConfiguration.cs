using Demo.DataAccess.model.EmployeeModel;
using Demo.DataAccess.model.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configurations
{
    public class EmployeeConfiguration : BaseConfiguration<Employee>,IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)");
            builder.Property(E=>E.Address).HasColumnType("varchar(150)");
            builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
            
            //HasConversion   we use it when use the Enum  to take two parameter
              // the first : to convert from Enum to string   to deal with in database 
              // the secound :to convert the string to enum again  to deal with in program 

            builder.Property(E => E.EmpGender)
                    .HasConversion((empGender) => empGender.ToString(),
                                  (_gender) => (Gendr)Enum.Parse(typeof(Gendr), _gender));

            //HasConversion   we use it when use the Enum  to take two parameter
            // the first : to convert from Enum to string   to deal with in database 
            // the secound :to convert the string to enum again  to deal with in program 

            builder.Property(E => E.EmployeeType)
                             .HasConversion((emptype) => emptype.ToString(),
                                               (_emptyp) => (EmployeeType)Enum.Parse(typeof(EmployeeType), _emptyp));
        
            base.Configure(builder);
        }

    }
}
