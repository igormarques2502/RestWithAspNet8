using RestWithAspNetUdemy.Hypermedia;
using RestWithAspNetUdemy.Hypermedia.Abstract;

namespace RestWithAspNetUdemy.Data.VO
{
    public class BookVO : ISupportsHypermedia
    {
        public long Id { get; set; }
        public required string Author { get; set; }
        public DateTime LaunchDate { get; set; }
        public decimal Price { get; set; }
        public required string Title { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
