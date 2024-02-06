using RestWithAspNetUdemy.Data.Converter.Implementations;
using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Repository;

namespace RestWithAspNetUdemy.Services.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }
        public PersonVO FindById(long Id)
        {
            return _converter.Parse(_repository.FindById(Id));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
           
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        //Method responsible for disable a person from ID
        public PersonVO Disable(long id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        
    }
}
