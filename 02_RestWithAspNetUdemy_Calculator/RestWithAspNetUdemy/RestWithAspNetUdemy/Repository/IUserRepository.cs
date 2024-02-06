using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Model;

namespace RestWithAspNetUdemy.Repository
{
    public interface IUserRepository
    {
        User? ValidateCredential(UserVO user);
        User? RefreshUserInfo(User user);
    }
}
