using BeerManagement.Application.Entities;
using BeerManagement.Application.Models;
using System.ComponentModel.DataAnnotations;

namespace BeerManagement.WebApi.Models.RequestModel
{
    public class AddBeerRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        

        public BeerEntity MapToEntity()
        {
            var beer = new BeerEntity
            {
                Name = Name,
                Type = Type
            };

            return beer;
        }
    }
}
