using AspWebApi.net.Models.Domain;

namespace AspWebApi.net.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
