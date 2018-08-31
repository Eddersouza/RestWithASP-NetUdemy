using RestWithASPNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNetUdemy.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);

        Book FindById(long id);

        List<Book> FindAll();

        Book Update(Book book);

        void Delete(long id);
    }
}