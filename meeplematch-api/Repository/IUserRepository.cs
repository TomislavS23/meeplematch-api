using meeplematch_api.DTO;

namespace meeplematch_api.Repository;

public interface IUserRepository
{
    IEnumerable<UserDTO> GetUsers();
    UserDTO GetUser(int id);
    void UpdateUser(UserDTO user, int id);
    void DeleteUser(int id);
}