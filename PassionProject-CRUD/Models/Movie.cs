using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PassionProject_CRUD.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string MovieGenre { get; set; }
        public DateTime MovieDate { get; set; }

        //Cost is in $
        public decimal MovieCost { get; set; }
        public string Moviedescription { get; set; }
    }

    public class MovieDto
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string MovieGenre { get; set; }
        public DateTime MovieDate { get; set; }

        //Cost is in $
        public decimal MovieCost { get; set; }
        public string Moviedescription { get; set; }
    }
}