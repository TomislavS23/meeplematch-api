using meeplematch_api.DTO;
using meeplematch_api.Model;

namespace meeplematch_api.Repository;

public interface IRoleRepository
{
    void InsertRole(RoleDTO role);
    void DeleteRole(int id);
    void UpdateRole(RoleDTO role, int id);
    RoleDTO GetRole(int id);
    IEnumerable<RoleDTO> GetRoles();
}