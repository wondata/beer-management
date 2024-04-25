using BeerManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BeerManagement.Application.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
        Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class;
        //Task<IEnumerable<T>> GetAllAsync<T>(Func<T, bool> predicate) where T : class;
        Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T model) where T : class;

        Task SaveChanges();
    }
}
