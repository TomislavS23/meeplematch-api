using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;
using meeplematch_api.Security;

namespace meeplematch_api.Service;

public class UserRepository : IUserRepository
{
    private readonly MeepleMatchContext _context;
    private readonly IMapper _mapper;

    public UserRepository(MeepleMatchContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public IEnumerable<UserDTO> GetUsers() => _mapper.Map<IEnumerable<UserDTO>>(_context.Users.ToList());

    public UserDTO GetUser(int id)
    {
        var entity = _context.Users.FirstOrDefault(u => u.IdUser == id);
        return _mapper.Map<UserDTO>(entity);
    }

    public void UpdateUser(CreateUserDTO user, int id)
    {
        var entity = _context.Users.FirstOrDefault(u => u.IdUser == id);

        entity.Username = user.Username ?? entity.Username;
        entity.Email = user.Email ?? entity.Email;
        entity.RoleId = (int)(user.RoleId != 0 ? user.RoleId : entity.RoleId);
        entity.FirstName = user.FirstName ?? entity.FirstName;
        entity.LastName = user.LastName ?? entity.LastName;
        entity.ImagePath = user.ImagePath ?? entity.ImagePath;
        entity.IsMale = user.IsMale ?? entity.IsMale;
        entity.UpdatedAt = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(user.Password))
        {
            var result = Encryption.Encrypt(user.Password);
            entity.HashedPassword = result.Key;
            entity.Salt = result.Value;
        }

        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var entity = _context.Users.FirstOrDefault(u => u.IdUser == id);
        _context.Users.Remove(entity);
        _context.SaveChanges();
    }

    public void CreateUser(CreateUserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);

        var result = Encryption.Encrypt(userDto.Password);

        user.HashedPassword = result.Key;
        user.Salt = result.Value;
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        user.IsBanned = false; 

        _context.Users.Add(user);
        _context.SaveChanges();
    }
}