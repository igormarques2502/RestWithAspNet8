using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Model.Base;

namespace RestWithAspNetUdemy.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long Id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long Id);
        bool Exists(long id);
        List<T> FindWithPagedSearch(string query);
        int GetCount(string query);
    }
}
