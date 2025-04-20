using Demo.DataAccess.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Demo.DataAccess.Repositories.Interface;

namespace Demo.DataAccess.Repositories.Class
{
    public class DepartmentRepository(ApplicationDbContext dbContext) :GenericRepository<Department>(dbContext) ,IDepartmentRepository
    {
       
     

    }
}
