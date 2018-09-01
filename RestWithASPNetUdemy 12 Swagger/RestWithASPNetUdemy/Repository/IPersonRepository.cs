using RestWithASPNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNetUdemy.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);

        Person FindById(long id);

        List<Person> FindAll();

        Person Update(Person person);

        void Delete(long id);

        bool Exists(long id);
    }
}