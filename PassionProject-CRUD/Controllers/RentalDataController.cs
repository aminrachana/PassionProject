using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject_CRUD.Models;
using System.Diagnostics;
using PassionProject_CRUD.Migrations;

namespace PassionProject_CRUD.Controllers
{
    public class RentalDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RentalData/ListRentals
        //curl https://localhost:44395/api/rentaldata/listrentals
        [HttpGet]
        public IEnumerable<RentalDto> ListRentals()
        {
            List<Rental> Rentals = db.Rental.ToList();
            List<RentalDto> RentalDtos = new List<RentalDto>();

            Rentals.ForEach(a => RentalDtos.Add(new RentalDto()
            {
                RentalId = a.RentalId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PuchaseDate = a.PuchaseDate,
                ReturnDate = a.ReturnDate,
                MovieName = a.Movie.MovieName,
                MovieGenre = a.Movie.MovieGenre,
                MovieDate = a.Movie.MovieDate,
                MovieCost = a.Movie.MovieCost,
                Moviedescription = a.Movie.Moviedescription

            }));

            return RentalDtos;
        }

        // GET: api/RentalData/FindRental/5
        //curl https://localhost:44395/api/rentaldata/findrental/33
        [ResponseType(typeof(Rental))]
        [HttpGet]
        public IHttpActionResult FindRental(int id)
        {
            Rental Rental = db.Rental.Find(id);
            RentalDto RentalDto = new RentalDto()
            {
                RentalId = Rental.RentalId,
                FirstName = Rental.FirstName,
                LastName = Rental.LastName,
                PuchaseDate = Rental.PuchaseDate,
                ReturnDate = Rental.ReturnDate,
                MovieName = Rental.Movie.MovieName,
                MovieGenre = Rental.Movie.MovieGenre,
                MovieDate = Rental.Movie.MovieDate,
                MovieCost = Rental.Movie.MovieCost,
                Moviedescription = Rental.Movie.Moviedescription

            };

            if (Rental == null)
            {
                return NotFound();
            }

            return Ok(RentalDto);
        }

        // POST: api/RentalData/UpdateRental/5
        //curl -d @rental.json -H "Content-type:application/json" "https://localhost:44395/api/rentaldata/updaterental/31"
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRental(int id, Rental rental)
        {
            Debug.WriteLine("I have reached the update rental method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != rental.RentalId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + rental.RentalId);
                Debug.WriteLine("POST parameter" + rental.FirstName);
                Debug.WriteLine("POST parameter" + rental.LastName);
                Debug.WriteLine("POST parameter" + rental.PuchaseDate);
                Debug.WriteLine("POST parameter" + rental.ReturnDate);
                return BadRequest();
            }

            db.Entry(rental).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    Debug.WriteLine("Rental not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RentalData/AddRental
        //curl -d @rental.json -H "Content-type:application/json" https://localhost:44395/api/rentaldata/addrental
        [ResponseType(typeof(Rental))]
        [HttpPost]
        public IHttpActionResult AddRental(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rental.Add(rental);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rental.RentalId }, rental);
        }

        // POST: api/RentalData/DeleteRental/5
        //curl -d "" https://localhost:44395/api/rentaldata/deleterental/33
        [ResponseType(typeof(Rental))]
        [HttpPost]

        public IHttpActionResult DeleteRental(int id)
        {
            Rental rental = db.Rental.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            db.Rental.Remove(rental);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalExists(int id)
        {
            return db.Rental.Count(e => e.RentalId == id) > 0;
        }
    }
}