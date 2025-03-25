using AutoMapper;
using meeplematch_api.DTO;
using meeplematch_api.Model;
using meeplematch_api.Repository;

namespace meeplematch_api.Service;

public class RoleRepository : IRoleRepository
{
    private readonly MeepleMatchContext _context;
    private readonly IMapper _mapper;

    public RoleRepository(MeepleMatchContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void InsertRole(RoleDTO role)
    {
        _context.Roles.Add(_mapper.Map<Role>(role));
        _context.SaveChanges();
    }

    public void DeleteRole(int id)
    {
        var role = _context.Roles.FirstOrDefault(r => r.IdRole == id);
        _context.Roles.Remove(role);
        _context.SaveChanges();
    }

    public void UpdateRole(RoleDTO role, int id)
    {
        var roleEntity = _context.Roles.FirstOrDefault(r => r.IdRole == id);
        roleEntity.Name = role.Name;
        _context.SaveChanges();
    }

    public RoleDTO GetRole(int id)
    {
        var role = _context.Roles.FirstOrDefault(r => r.IdRole == id);
        return _mapper.Map<RoleDTO>(role);
    }

    public IEnumerable<RoleDTO> GetRoles()
    {
        var roles = _context.Roles.ToList();
        return _mapper.Map<IEnumerable<RoleDTO>>(roles);
    }
}