using meeplematch_api.DTO;

namespace meeplematch_api.Repository;

public interface IUserRepository
{
    IEnumerable<UserDTO> GetUsers();
    UserDTO GetUser(int id);
    void UpdateUser(CreateUserDTO user, int id);
    void DeleteUser(int id);
    void CreateUser(CreateUserDTO userDto);
}