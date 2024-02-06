using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Hypermedia.Utils;
using RestWithAspNetUdemy.Model;

namespace RestWithAspNetUdemy.Services
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long Id);
        List<PersonVO> FindPersonByName(string firstName, string lastName);
        List<PersonVO> FindAll();
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        PersonVO Update(PersonVO person);
        PersonVO Disable(long id);
        void Delete(long Id);

    }
}
