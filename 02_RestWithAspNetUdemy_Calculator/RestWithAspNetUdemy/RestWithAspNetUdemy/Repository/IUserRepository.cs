using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Model;

namespace RestWithAspNetUdemy.Repository
{
    public interface IUserRepository
    {
        User? ValidateCredential(UserVO user);

        User? ValidateCredential(string username);

        bool RevokeToken(string username);

        User? RefreshUserInfo(User user);
    }
}
