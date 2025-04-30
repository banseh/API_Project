using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketSys.DAL
{

    public class GeniricRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly BugticketContext _context;

        public GeniricRepo(BugticketContext context_)
        {
            _context = context_;
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>()
                .AddAsync(entity);
        }
        public async Task Delete(T Entity)
        {
             _context.Set<T>()
                .Remove(Entity);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();

        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>()
                .FindAsync(id);
        }

        public async Task Update(T project)
        {

        }



        //public async Task<int> savechangesAsync()
        //{
        //    return await _context.SaveChangesAsync();
        //}
    }

}
