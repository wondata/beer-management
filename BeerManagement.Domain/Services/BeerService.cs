using BeerManagement.Application.Entities;
using BeerManagement.Application.Interfaces;
using BeerManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerManagement.Domain.Services
{
    public class BeerService : IBeerService
    {
        private readonly IGenericRepository<Beer> _beerRepository;

        public BeerService(IGenericRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerEntity>> GetAllBeers(string? searchParam)
        {
            var bms = await _beerRepository.GetAllAsync();
            if (bms != null && !string.IsNullOrWhiteSpace(searchParam))
            {
                bms = bms.Where(b => b.Name.Contains(searchParam, StringComparison.InvariantCultureIgnoreCase));
            }
            var beers = bms?.Select(b => new BeerEntity(b)).ToList();
            return beers ?? new List<BeerEntity>();
        }

        public List<string> GetBeerTypes()
        {
            var beerTypes = new List<string>
            {
                "Pale ale", 
                "Stout"
            };

            return beerTypes;
        }

        public async Task AddBeer(BeerEntity beer)
        {
            //Check for validity of beer type
            var isValidType = GetBeerTypes().Contains(beer.Type, StringComparer.InvariantCultureIgnoreCase);
            if(!isValidType)
            {
                throw new ArgumentException("Invalid Beer type");
            }

            var bm = beer.MapToModel();
            await _beerRepository.AddAsync(bm);
        }

        public async Task UpdateRating(int id)
        {
            //Get Beer
            var beer = await _beerRepository.GetAsync(b => b.Id == id);
            if (beer == null)
            {
                throw new KeyNotFoundException("Failed to update beer rating: Beer not found");
            }

            //Get beers average rating
            var beers = await _beerRepository.GetAllAsync();
            var averageRating = beers?.Average(b => b.Rating) ?? 0;

            beer.Rating = (int)Math.Round(averageRating, MidpointRounding.AwayFromZero);
            beer.UpdatedAt = DateTime.Now;
            await _beerRepository.UpdateAsync(beer);

        }

        public void DeleteBeer(Beer entity)
        {
            throw new NotImplementedException();
        }

        

        
    }
}
