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
using PassionProject_CRUD.Migrations;
using PassionProject_CRUD.Models;

namespace PassionProject_CRUD.Controllers
{
    public class RentalDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RentalData/ListRental
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
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRental(int id, Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rental.RentalId)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RentalData/AddRental
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

            return Ok(rental);
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