using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithASPNetUdemy.Business.Implementation
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IRepository<Person> _personRepository;

        public PersonBusinessImpl(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(Person person)
        {
            return _personRepository.Create(person);
        }

        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }

        public List<Person> FindAll()
        {
            return _personRepository.FindAll();
        }

        public Person FindById(long id)
        {
            return _personRepository.FindById(id);
        }

        public Person Update(Person person)
        {
            return _personRepository.Update(person);
        }
    }
}