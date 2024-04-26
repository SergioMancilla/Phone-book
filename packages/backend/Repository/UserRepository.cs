using backend.DAL;
using backend.Models;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class UserRepository : BaseRepository, IUserRepository
{
    private readonly PasswordHashService _passwordHashService;


    public UserRepository(PhonebookContext context) : base(context) {
        _passwordHashService = new PasswordHashService();
    }

    public User? GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public async Task<Boolean> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            return false;
        }

        if (!_passwordHashService.VerifyPassword(user.Password, password))
        {
            return false;
        }
        return true;
    }

    public User Register(string name, string email, string password)
    {
        var securePassword = _passwordHashService.HashPassword(password);
        var user = new User { Name = name, Email = email, Password = securePassword };

        

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }



}
