using RestWithASPNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNetUdemy.Services
{
    public interface IPersonService
    {
        Person Create(Person person);

        Person FindById(long id);

        List<Person> FindAll();

        Person Update(Person person);

        void Delete(long id);
    }
}