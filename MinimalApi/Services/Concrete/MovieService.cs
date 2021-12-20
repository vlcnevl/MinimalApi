using MinimalApi.Models;
using MinimalApi.Repositories;

namespace MinimalApi.Services
{
    public class MovieService : IMovieService
    {
        public Movie Create(Movie movie)
        {
            movie.Id = MovieRepository.Movies.Count + 1;
            MovieRepository.Movies.Add(movie);
            return movie;
        }

        public bool Delete(int id)
        {
            var movie = MovieRepository.Movies.FirstOrDefault(m=> m.Id == id);
            if (movie == null) return false;

            MovieRepository.Movies.Remove(movie);
            return true;

                
        }

        public Movie Get(int id)
        {
            var movie = MovieRepository.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)  return null;
            return movie;
        }

        public List<Movie> GetAll()
        {
            var movies = MovieRepository.Movies;
            return movies;
        }

        public Movie Update(Movie movie)
        {
            var oldMovie = MovieRepository.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (oldMovie == null) return null;

            oldMovie.Title = movie.Title;  
            oldMovie.Description = movie.Description;  
            oldMovie.Rating = movie.Rating;
            return movie;
        }
    }
}
