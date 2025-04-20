using Demo.DataAccess.Data.Context;
using Demo.DataAccess.model.EmployeeModel;
using Demo.DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Class
{
    public class EmployeeRepository(ApplicationDbContext dbContext) : GenericRepository<Employee>(dbContext),IEmployeeRepo
    {
    }
}     


