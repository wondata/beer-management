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

namespace BeerManagement.Infrastructure.Repositories
{
    public class BeerRepository: IGenericRepository<Beer>
    {
        private readonly BeerDbContext _context;

        public BeerRepository(BeerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> GetAllAsync()
        {
            var query = _context.Set<Beer>().AsNoTracking();
            return query;
        }

        public async Task<Beer?> GetAsync(Func<Beer, bool> predicate)
        {
            var query = (await GetAllAsync()).FirstOrDefault(predicate);
            return query;
        }

        public async Task AddAsync(Beer model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            await _context.Set<Beer>().AddAsync(model);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Beer model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            _context.Set<Beer>().Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
