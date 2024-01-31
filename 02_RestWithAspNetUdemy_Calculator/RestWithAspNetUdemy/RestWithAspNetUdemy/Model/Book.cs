using RestWithAspNetUdemy.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Schema;

namespace RestWithAspNetUdemy.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Column("author")]
        public required string Author { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("title")]
        public required string Title { get; set; }

    }
}
