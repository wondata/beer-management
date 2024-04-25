using BeerManagement.Application.Entities;
using BeerManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerManagement.Application.Interfaces
{
    public interface IBeerService
    {
        Task<IEnumerable<BeerEntity>> GetAllBeers(string? searchParam);
        Task<List<string>> GetBeerTypes();
        Task AddBeer(BeerEntity entity);
        Task UpdateRating(RatingEntity rating);
    }
}
