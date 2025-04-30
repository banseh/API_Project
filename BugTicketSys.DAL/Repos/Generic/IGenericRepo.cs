using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketSys.DAL
{

    public interface IGenericRepo<T> where T : class
    {

        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }


}
