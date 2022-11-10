using AspWebApi.net.Models.Domain;

namespace AspWebApi.net.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
