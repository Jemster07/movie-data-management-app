using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Models;
using MoviesApi.Repositories;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository repository)
        {
            _movieRepository = repository;
        }

        // List all movies
        [HttpGet]
        public ActionResult<List<Movie>> GetMovies()
        {
            try
            {
                List<Movie> results = _movieRepository.GetMovies();

                if (results.Count <= 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(results);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // Get movie by ID
        [HttpGet("{id}")]
        public Movie GetMovieById(int id)
        {
            return _movieRepository.GetMovieById(id);
        }

        // Update movie - must have exception handling
        [HttpPut("{id}")]
        public ActionResult<Movie> UpdateMovie(Movie payload, int id)
        {
            try
            {
                if (!_movieRepository.MovieExists(id))
                {
                    return BadRequest("Movie not found or is not recorded.");
                }
                else
                {
                    payload.ID = id;
                    return Ok(_movieRepository.UpdateMovie(payload));
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // Add new movie
        [HttpPost]
        public ActionResult<bool> InsertMovie(Movie payload)
        {
            try
            {
                bool inserted = _movieRepository.InsertMovie(payload);

                if (!inserted)
                {
                    return BadRequest("An error occured while adding the movie.");
                }
                else
                {
                    return Ok(inserted);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // Delete movie
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteMovie(int id)
        {
            try
            {
                bool deleted = _movieRepository.DeleteMovie(id);

                if (!deleted)
                {
                    return BadRequest("An error occurred while deleting the movie.");
                }
                else
                {
                    return Ok(deleted);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // Check if movie exists
        [HttpGet("status/{id}")]
        public ActionResult<bool> MovieExists(int id)
        {
            try
            {
                bool exists = _movieRepository.MovieExists(id);

                if (!exists)
                {
                    return Ok("Movie does not exist.");
                }
                else
                {
                    return Ok("Movie exists.");
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
