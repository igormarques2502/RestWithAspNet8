using RestWithAspNetUdemy.Hypermedia.Abstract;

namespace RestWithAspNetUdemy.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();

    }
}
