using RestWithAspNetUdemy.Configurations;
using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Repository;
using RestWithAspNetUdemy.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestWithAspNetUdemy.Business.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy/dd/mm HH:mm:ss";
        private TokenConfigurations _configuration;
        private IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginBusiness(TokenConfigurations configuration, IUserRepository userRepository, ITokenService tokenService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredential(UserVO userCredentials)
        {
            var user = _userRepository.ValidateCredential(userCredentials);

            if (user == null) return null;
            var claims = new List<Claim> {

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpire);

            _userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken);
        }
    }
}


