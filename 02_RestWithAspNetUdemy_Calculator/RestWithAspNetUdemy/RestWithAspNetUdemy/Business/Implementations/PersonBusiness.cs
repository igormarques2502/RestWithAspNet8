using RestWithAspNetUdemy.Data.Converter.Implementations;
using RestWithAspNetUdemy.Data.VO;
using RestWithAspNetUdemy.Hypermedia.Utils;
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
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            
            
            var sort = !string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = pageSize < 1 ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query += $"and p.first_name like '%" + name + "%'";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";

            string countQuery = "select count(*) from Person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery += $" and p.first_name like '%" + name + "%'";

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);
            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }

        public PersonVO FindById(long Id)
        {
            return _converter.Parse(_repository.FindById(Id));
        }
        public List<PersonVO> FindPersonByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
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
