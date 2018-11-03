using RestWithASPNetUdemy.Data.Converters;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithASPNetUdemy.Business.Implementation
{
    public class BookBusinessImpl : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        private readonly BookConverter _bookConverter;

        public BookBusinessImpl(
            IRepository<Book> repository)
        {
            _repository = repository;
            _bookConverter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            var entityBook = _bookConverter.Parse(book);
            entityBook = _repository.Create(entityBook);
            return _bookConverter.Parse(entityBook);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _bookConverter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _bookConverter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            var entityBook = _bookConverter.Parse(book);
            entityBook = _repository.Update(entityBook);
            return _bookConverter.Parse(entityBook);
        }
    }
}