using meeplematch_api.DTO;

namespace meeplematch_api.Repository;

public interface IAuthRepository
{
    string Login(string username, string password);
    string Register(RegisterDTO register);
    void ChangePassword(string username, string oldPassword, string newPassword);
}