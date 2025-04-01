using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;

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

    public void UpdateUser(UserDTO user, int id)
    {
        var entity = _context.Users.FirstOrDefault(u => u.IdUser == id);
        entity.Username = user.Username;
        entity.Email = user.Email;
        entity.RoleId = user.RoleId;
        entity.IsBanned = user.IsBanned;
        entity.UpdatedAt = user.UpdatedAt;
        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var entity = _context.Users.FirstOrDefault(u => u.IdUser == id);
        _context.Users.Remove(entity);
        _context.SaveChanges();
    }
}