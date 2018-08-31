using RestWithASPNetUdemy.Data.Converters;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository.Generic;
using System.Collections.Generic;

namespace RestWithASPNetUdemy.Business.Implementation
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IRepository<Person> _personRepository;

        private readonly PersonConverter _personConverter;

        public PersonBusinessImpl(
            IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
            _personConverter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _personConverter.Parse(person);
            personEntity = _personRepository.Create(personEntity);

            return _personConverter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _personConverter.Parse(_personRepository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _personConverter.Parse(_personRepository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _personConverter.Parse(person);
            personEntity = _personRepository.Update(personEntity);

            return _personConverter.Parse(personEntity);
        }
    }
}