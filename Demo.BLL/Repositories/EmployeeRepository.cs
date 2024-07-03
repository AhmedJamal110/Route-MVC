using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenaricRepository<Employee> , IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository( ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public IQueryable<Employee> GetEmployessByName(string name)
        => _context.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));
        
    }
}
