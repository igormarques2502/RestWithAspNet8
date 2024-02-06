using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetUdemy.Data.VO
{
    [Table("users")]
    public class UserVO
    {
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("password")]
        public string Password {  get; set; }
    }
}
