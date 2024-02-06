using Microsoft.IdentityModel.Tokens;
using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Model.Context;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace RestWithAspNetUdemy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredential(UserVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == pass);
        }

        public User ValidateCredential(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public User? RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(u => (u.UserName == username));
            if (user == null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            var sBuilder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                sBuilder.Append(item.ToString("x2"));
            }

            return sBuilder.ToString();
        }


    }
}
