using AutoMapper;
using BeerManagement.Application.Entities;
using BeerManagement.Application.Interfaces;
using BeerManagement.Application.Models;
using System.Linq;

namespace BeerManagement.Domain.Services
{
    public class BeerService : IBeerService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public BeerService(IRepository beerRepository, IMapper mapper)
        {
            _repository = beerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BeerEntity>> GetAllBeers(string? searchParam)
        {
            var bms = await _repository.GetAllAsync<Beer>(b => b.Ratings);
            if (bms != null && !string.IsNullOrWhiteSpace(searchParam))
            {
                bms = bms.Where(b => b.Name.Contains(searchParam, StringComparison.InvariantCultureIgnoreCase));
            }
            var beers = bms?.Select(b => _mapper.Map<BeerEntity>(b)).ToList();
            return beers ?? [];
        }

        public async Task<List<string>> GetBeerTypes()
        {
            var bts = await _repository.GetAllAsync<BeerType>();

            if(bts.Count() > 0)
            {
                return bts.Select(b=>b.Name).ToList();
            } else
            {
                await _repository.AddAsync<BeerType>(new BeerType { Name = "Pale ale" });
                await _repository.AddAsync<BeerType>(new BeerType { Name = "Stout" });
                bts = await _repository.GetAllAsync<BeerType>();
            }

            return bts?.Select(b=> b.Name).ToList() ?? [];
        }

        public async Task AddBeer(BeerEntity beer)
        {
            //Check for validity of beer type
            var isValidType = (await GetBeerTypes()).Contains(beer.Type, StringComparer.InvariantCultureIgnoreCase);
            if(!isValidType)
            {
                throw new ArgumentException("Invalid Beer type");
            }

            var bm = _mapper.Map<Beer>(beer);
            await _repository.AddAsync<Beer>(bm);
        }

        public async Task UpdateRating(RatingEntity rating)
        {
            //Get Beer
            var beer = (await _repository.GetAsync<Beer>(b => b.Id == rating.BeerId, i => i.Ratings));
            if (beer == null)
            {
                throw new KeyNotFoundException("Failed to update beer rating: Beer not found");
            }

            //Add new rating
            var rm = _mapper.Map<Rating>(rating);
            beer.Ratings?.Add(rm);

            var averageRating = beer.Ratings?.Average(r => r?.Rate) ?? 0;
            beer.Rating = (int)Math.Round(averageRating, MidpointRounding.AwayFromZero);
            beer.UpdatedAt = DateTime.Now;

            await _repository.SaveChanges();
        }

    }
}
