using backend.Models.DTO;
using backend.Models;

namespace backend.DAL;

public interface IUserRepository : IDisposable
{
    Task<Boolean> Login(string username, string password);
    User Register(string name, string username, string password);
    User? GetByEmail(string email);
}
