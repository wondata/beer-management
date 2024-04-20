using BeerManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerManagement.Application.Entities
{
    public class BeerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Rating { get; set; }

        public BeerEntity()
        {
            
        }

        public BeerEntity(Beer beer)
        {
            Id = beer.Id;
            Name = beer.Name;
            Type = beer.Type;
            Rating = beer.Rating;
        }

        public Beer MapToModel()
        {
            var beerModel = new Beer
            {
                Name = Name,
                Type = Type,
                Rating = Rating
            };

            return beerModel;
        }
    }

    
}
