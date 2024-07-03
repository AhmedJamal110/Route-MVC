using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenaricRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenaricRepository( ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task Add(T item)
        {
            await _context.Set<T>().AddAsync(item);
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return   (IEnumerable<T>)await _context.Employees.Include(e => e.department).ToListAsync();
            }
         
              
            return await _context.Set<T>().ToListAsync();
            
            

        }

        public async Task<T> GetById(int id)
        =>  await _context.Set<T>().FindAsync(id);
        

        public void Uodate(T item)
        {
            _context.Set<T>().Update(item);
        }
    }
}
