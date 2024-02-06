using RestWithAspNetUdemy.Model;

namespace RestWithAspNetUdemy.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);

        List<Person> FindByName(string firstName, string lastName);
    }
}
