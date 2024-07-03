using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUintOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public UnitOfWork(ApplicationDbContext context)
        {
            EmployeeRepository = new EmployeeRepository(context);
            DepartmentRepository = new DepartmentRepository(context);
            _context = context;
        }

        public async Task<int> Complete()
            => await _context.SaveChangesAsync();


        public void Dispose()
            => _context.Dispose();

  
    
            
     }
}
