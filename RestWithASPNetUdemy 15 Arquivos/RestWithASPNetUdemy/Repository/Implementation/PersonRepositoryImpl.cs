using System.Collections.Generic;
using System.Linq;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Model.Context;
using RestWithASPNetUdemy.Repository.Generic;

namespace RestWithASPNetUdemy.Repository.Implementation
{
    public class PersonRepositoryImpl : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepositoryImpl(MySQLContext mySQLContext)
            : base(mySQLContext)
        {
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                return _context.Persons
                    .Where(p => p.FirstName.Contains(firstName)
                    && p.LastName.Contains(lastName)).ToList();

            if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                return _context.Persons
                    .Where(p => p.FirstName.Contains(firstName)).ToList();

            if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                return _context.Persons
                    .Where(p => p.LastName.Contains(lastName)).ToList();

            return _context.Persons.ToList();
        }
    }
}