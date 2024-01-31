using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithAspNetUdemy.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
