using MinimalApi.Models;

namespace MinimalApi.Repositories
{
    public class UserRepository //data
    {
        public static List<User> Users = new()
        {
            new User { Name ="Velican", Surname="Evli", EmailAddress="veli@gmail.com",Password="123456",UserName="vlcnevl",Role="Admin"},
            new User { Name = "Cem", Surname = "Büyük", EmailAddress = "cembuyuk@gmail.com", Password = "123456", UserName = "cembuyuk", Role = "Standart" },
        };
    }
}
