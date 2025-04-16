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
    public class EmployeeRepository(ApplicationDbContext dbContext):IEmployeeRepo
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        //we will make the crude system 

        //Get All
        public IEnumerable<Employee> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.employees.ToList();
            }
            return _dbContext.employees.AsNoTracking().ToList();
        }


        //Get By ID 
        public Employee? GetById(int id) => _dbContext.employees.Find(id);


        //Update 

        public int Update(Employee Employee)
        {
            _dbContext.employees.Update(Employee);
            return _dbContext.SaveChanges();
        }

        //Delete 
        public int Remove(Employee Employee)
        {
            _dbContext.employees.Remove(Employee);
            return _dbContext.SaveChanges();
        }

        //Insert

        public int Add(Employee Employee)
        {
            _dbContext.employees.Add(Employee);
            return _dbContext.SaveChanges();
        }

    }
}     



