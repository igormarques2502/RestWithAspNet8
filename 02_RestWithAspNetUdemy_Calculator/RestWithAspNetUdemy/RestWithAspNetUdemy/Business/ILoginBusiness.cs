using RestWithAspNetUdemy.Data.VO;

namespace RestWithAspNetUdemy.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredential(UserVO user);
        TokenVO ValidateCredential(TokenVO token);
        Boolean RevokeToken(string username);
    }
}
