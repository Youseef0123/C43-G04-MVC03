using Demo.DataAccess.Data.Context;
using Demo.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Class
{
    //we convert the unit of work to work as lazy loading to do not make the all repositroy when call one from repository 

    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepo> _employeeRepository;
        private readonly ApplicationDbContext _dbcontext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._dbcontext = dbContext;
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepo>(()=>new EmployeeRepository(dbContext));

        }

        public IEmployeeRepo EmployeeRepo => _employeeRepository.Value;
        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;
        public int SaveChangs()
        {
            return _dbcontext.SaveChanges();    
        }
    }
}



   