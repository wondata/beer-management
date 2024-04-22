using BeerManagement.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BeerManagement.Application.Entities
{
    public class RatingEntity
    {
        public int Id { get; set; }
        public int BeerId { get; set; }
        public int Rate { get; set; }

        public RatingEntity()
        {
            
        }

        public RatingEntity(Rating rating)
        {
            Id = rating.Id;
            BeerId = rating.BeerId;
            Rate = rating.Rate;
        }

        public Rating MapToModel()
        {
            var ratingModel = new Rating
            {
                BeerId = BeerId ,
                Rate = Rate
            };

            return ratingModel;
        }
    }
}
