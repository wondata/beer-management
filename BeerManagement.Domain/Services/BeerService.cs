using BeerManagement.Application.Entities;
using BeerManagement.Application.Interfaces;
using BeerManagement.Application.Models;

namespace BeerManagement.Domain.Services
{
    public class BeerService : IBeerService
    {
        private readonly IRepository _repository;

        public BeerService(IRepository beerRepository)
        {
            _repository = beerRepository;
        }

        public async Task<IEnumerable<BeerEntity>> GetAllBeers(string? searchParam)
        {
            var bms = await _repository.GetAllAsync<Beer>(b => b.Ratings);
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
            bm.EnteredAt = DateTime.Now;
            await _repository.AddAsync<Beer>(bm);
        }

        public async Task UpdateRating(RatingEntity rating)
        {
            //Get Beer
            var beer = await _repository.GetAsync<Beer>(b => b.Id == rating.BeerId);
            if (beer == null)
            {
                throw new KeyNotFoundException("Failed to update beer rating: Beer not found");
            }

            //Add new rating
            var rm = rating.MapToModel();
            await _repository.AddAsync<Rating>(rm);

            //Get beers average rating
            var ratings = await _repository.GetAllAsync<Rating>(r=> r.BeerId == rating.BeerId);
            var averageRating = ratings?.Average(b => b.Rate) ?? 0;

            beer.Rating = (int)Math.Round(averageRating, MidpointRounding.AwayFromZero);
            beer.UpdatedAt = DateTime.Now;
            await _repository.UpdateAsync<Beer>(beer);

        }

    }
}
