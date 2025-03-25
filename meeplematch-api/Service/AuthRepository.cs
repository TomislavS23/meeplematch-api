using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;
using meeplematch_api.Security;
using Microsoft.EntityFrameworkCore;

namespace meeplematch_api.Service;

public class AuthRepository : IAuthRepository
{
    private readonly MeepleMatchContext _context;
    private readonly IMapper _mapper;

    public AuthRepository(MeepleMatchContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public string Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        var role = _context.Roles
            .Include(u => u.Users)
            .Where(u => u.Users.FirstOrDefault(usr => usr.Username == username) != null)
            .Select(u => u.Name)
            .FirstOrDefault();

        if (user == null) return null;

        var result = Encryption.Encrypt(password, user.Salt);

        if (Enumerable.SequenceEqual(result.Key, user.HashedPassword))
        {
            return TokenUtils.GenerateToken(user.Username, role);
        }

        return null;
    }

    public string Register(RegisterDTO register)
    {
        var user = _mapper.Map<User>(register);

        var result = Encryption.Encrypt(register.Password);

        user.HashedPassword = result.Key;
        user.Salt = result.Value;

        _context.Users.Add(user);
        _context.SaveChanges();

        var role = _context.Roles
            .Include(u => u.Users)
            .Where(u => u.Users.FirstOrDefault(usr => usr.Username == user.Username) != null)
            .Select(u => u.Name)
            .FirstOrDefault();

        return TokenUtils.GenerateToken(user.Username, role);
    }

    public void ChangePassword(string username, string oldPassword, string newPassword)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        var oldHashedPassword = Encryption.Encrypt(oldPassword, user.Salt);

        if (user.HashedPassword == oldHashedPassword.Key)
            user.HashedPassword = Encryption.Encrypt(newPassword).Key;
        
        _context.SaveChanges();
    }
}