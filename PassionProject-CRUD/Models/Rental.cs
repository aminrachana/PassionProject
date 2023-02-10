using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject_CRUD.Models
{
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PuchaseDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

    }

    public class RentalDto
    {
        public int RentalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PuchaseDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string MovieName { get; set; }
        public string MovieGenre { get; set; }
        public DateTime MovieDate { get; set; }

        //Cost is in $
        public decimal MovieCost { get; set; }
        public string Moviedescription { get; set; }
    }
}