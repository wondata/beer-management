using BeerManagement.Application.Entities;
using System.ComponentModel.DataAnnotations;

namespace BeerManagement.WebApi.Models.RequestModel
{
    public class UpdateRatingRequestModel
    {
        [Required]
        public int BeerId { get; set; }
        [Required]
        [Range(1,5)]
        public int Rate { get; set; }

        public RatingEntity MapToEntity()
        {
            var rating = new RatingEntity
            {
                BeerId = BeerId,
                Rate = Rate
            };

            return rating;
        }
    }
}
