using RestWithAspNetUdemy.Data.VO;

namespace RestWithAspNetUdemy.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredential(UserVO user);
    }
}
