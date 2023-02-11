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

namespace PassionProject_CRUD.Controllers
{
    public class MovieDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MovieData/ListMovies
        //curl https://localhost:44395/api/moviedata/listmovies
        [HttpGet]
        public IEnumerable<MovieDto> ListMovies()
        {
            List<Movie> Movies = db.Movies.ToList();
            List<MovieDto> MovieDtos = new List<MovieDto>();

            Movies.ForEach(a => MovieDtos.Add(new MovieDto()
            {
                MovieId = a.MovieId,
                MovieName = a.MovieName,
                MovieGenre = a.MovieGenre,
                MovieDate = a.MovieDate,
                MovieCost = a.MovieCost,
                Moviedescription = a.Moviedescription

            }));

            return MovieDtos;
        }

        // GET: api/MovieData/FindMovie/5
        //curl https://localhost:44395/api/moviedata/findmovie/4
        [ResponseType(typeof(Movie))]
        [HttpGet]
        public IHttpActionResult FindMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            MovieDto MovieDto = new MovieDto()
            {
                MovieId = movie.MovieId,
                MovieName = movie.MovieName,
                MovieGenre = movie.MovieGenre,
                MovieDate = movie.MovieDate,
                MovieCost = movie.MovieCost,
                Moviedescription = movie.Moviedescription
            };

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(MovieDto);
        }

        // POST: api/MovieData/UpdateMovie/5
        //curl -d @movie.json -H "Content-type:application/json" "https://localhost:44395/api/moviedata/updatemovie/11"
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMovie(int id, Movie movie)
        {
            Debug.WriteLine("I have reached the update movie method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != movie.MovieId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + movie.MovieId);
                Debug.WriteLine("POST parameter" + movie.MovieName);
                Debug.WriteLine("POST parameter" + movie.MovieGenre);
                Debug.WriteLine("POST parameter" + movie.MovieDate);
                Debug.WriteLine("POST parameter" + movie.MovieCost);
                Debug.WriteLine("POST parameter" + movie.Moviedescription);
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    Debug.WriteLine("Movie not found");
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

        // POST: api/MovieData/AddMovie
        //curl -d @movie.json -H "Content-type:application/json" https://localhost:44395/api/moviedata/addmovie
        [ResponseType(typeof(Movie))]
        [HttpPost]
        public IHttpActionResult AddMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movie.MovieId }, movie);
        }

        // POST: api/MovieData/DeleteMovie/5
        //curl -d "" https://localhost:44395/api/moviedata/deletemovie/8
        [ResponseType(typeof(Movie))]
        [HttpPost]

        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            db.SaveChanges();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.MovieId == id) > 0;
        }
    }
}