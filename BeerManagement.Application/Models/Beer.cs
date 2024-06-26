﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerManagement.Application.Models
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? Rating { get; set; }
        public DateTime EnteredAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        //One-to-many relationship
        public ICollection<Rating>? Ratings { get; set; }
    }
}
