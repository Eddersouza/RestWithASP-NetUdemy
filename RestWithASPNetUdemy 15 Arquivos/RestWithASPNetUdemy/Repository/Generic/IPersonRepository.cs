using RestWithASPNetUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNetUdemy.Repository.Generic
{
    public interface IPersonRepository : IRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);
    }
}