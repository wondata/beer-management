using BeerManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerManagement.Application.Entities
{
    public class BeerTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BeerTypeEntity()
        {

        }

        public BeerTypeEntity(BeerType beerType)
        {
            Id = beerType.Id;
            Name = beerType.Name;
        }

        public BeerType MapToModel()
        {
            var beerType = new BeerType
            {
                Name = Name,
            };

            return beerType;
        }

    }
}
