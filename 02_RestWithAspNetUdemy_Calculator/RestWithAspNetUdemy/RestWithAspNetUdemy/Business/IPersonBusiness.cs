using RestWithAspNetUdemy.Data.VO;

namespace RestWithAspNetUdemy.Services
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindById(long Id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        PersonVO Disable(long id);
        void Delete(long Id);
        
    }
}
