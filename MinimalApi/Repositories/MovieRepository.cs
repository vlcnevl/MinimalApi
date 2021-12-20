using MinimalApi.Models;

namespace MinimalApi.Repositories
{
    public class MovieRepository
    {
        public static List<Movie> Movies = new()
        {
            new Movie { Id = 1 ,Title = "The Shawshank Redemption", Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", Rating = 9.3},
            new Movie { Id = 2, Title = "The Godfather", Description = "The Godfather follows Vito Corleone, Don of the Corleone family, as he passes the mantel to his unwilling son, Michael.", Rating = 9.2 },
            new Movie { Id = 3, Title = "The Dark Knight", Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", Rating = 9.3 },
            new Movie { Id = 4, Title = "12 Angry Men", Description = "The jury in a New York City murder trial is frustrated by a single member whose skeptical caution forces them to more carefully consider the evidence before jumping to a hasty verdict.", Rating = 9.0 },
            new Movie { Id = 5, Title = "Schindler's List", Description = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", Rating = 8.9 }
 
        };
    }
}
