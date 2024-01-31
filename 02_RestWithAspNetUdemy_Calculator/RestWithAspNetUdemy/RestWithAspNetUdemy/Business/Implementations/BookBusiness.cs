using RestWithAspNetUdemy.Model;
using RestWithAspNetUdemy.Model.Context;
using RestWithAspNetUdemy.Repository;

namespace RestWithAspNetUdemy.Services.Implementations
{
    public class BookBusiness : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusiness(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }
        public Book FindById(long Id)
        {
            return _repository.FindById(Id);
        }

        public Book Create(Book book)
        {
           return _repository.Create(book);
           
        }
        public Book Update(Book book)
        {
            return _repository.Update(book);
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }
    }
}
