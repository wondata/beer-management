using BeerManagement.Application.Models;
using BeerManagement.Application.Interfaces;
using BeerManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;

namespace BeerManagement.Infrastructure.Repositories
{
    public class Repository: IRepository
    {
        private readonly BeerDbContext _context;

        public Repository(BeerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            var query = _context.Set<T>().AsNoTracking();
            return query;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            IQueryable<T> query = _context.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public async Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class
        {
            var query = (await GetAllAsync<T>(predicate,includeProperties)).FirstOrDefault();
            return query;
        }

        public async Task AddAsync<T>(T model) where T : class
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            await _context.Set<T>().AddAsync(model);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync<T>(T model) where T : class
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            _context.Set<T>().Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;

            _context.Dispose();
        }

    }
}
