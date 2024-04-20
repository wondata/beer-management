using BeerManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerManagement.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<Beer?> GetAsync(Func<Beer, bool> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(Beer model);
    }
}
