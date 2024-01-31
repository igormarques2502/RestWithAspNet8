using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Repository;

namespace RestWithAspNetUdemy.Services.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        public PersonBusiness(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }
        public Person FindById(long Id)
        {
            return _repository.FindById(Id);
        }

        public Person Create(Person person)
        {
           return _repository.Create(person);
           
        }
        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }
    }
}
