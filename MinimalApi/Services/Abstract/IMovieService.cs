using MinimalApi.Models;

namespace MinimalApi.Services
{
    public interface IMovieService
    {
        public Movie Create(Movie movie);
        public Movie Update(Movie movie);
        public bool Delete(int id);    
        public List<Movie> GetAll();
        public Movie Get(int id);
    }
}
