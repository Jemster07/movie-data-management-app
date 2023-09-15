using MoviesApi.Models;

namespace MoviesApi.Repositories
{
    public interface IMovieRepository
    {
        public List<Movie> GetMovies();
        public Movie GetMovieById(int id);
        public Movie UpdateMovie(Movie movie);
        public bool InsertMovie(Movie movie);
        public bool MovieExists(int id);
        public bool DeleteMovie(int id);
    }
}
