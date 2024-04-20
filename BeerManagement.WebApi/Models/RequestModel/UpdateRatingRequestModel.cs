using System.ComponentModel.DataAnnotations;

namespace BeerManagement.WebApi.Models.RequestModel
{
    public class UpdateRatingRequestModel
    {
        [Required]
        public int Id { get; set; }
    }
}
