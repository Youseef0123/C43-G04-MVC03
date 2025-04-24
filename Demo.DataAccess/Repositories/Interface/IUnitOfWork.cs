using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interface
{
    public interface IUnitOfWork
    {
        public IEmployeeRepo EmployeeRepo { get; }

        public IDepartmentRepository DepartmentRepository { get; }

        int SaveChangs();
    }
}
