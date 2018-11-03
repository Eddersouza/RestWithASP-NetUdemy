using RestWithASPNetUdemy.Data.Converters;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository.Generic;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestWithASPNetUdemy.Business.Implementation
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IPersonRepository _personRepository;

        private readonly PersonConverter _personConverter;

        public PersonBusinessImpl(
            IPersonRepository personRepository)
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

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string nome, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;
            string query = @"
                SELECT
                    *
                FROM
                    persons p
                WHERE
                    1=1 ";

            if (!string.IsNullOrEmpty(nome))
                query += $" AND p.FirstName LIKE '%{nome}%' ";

            query += $" ORDER BY p.FirstName {sortDirection} LIMIT {pageSize} offset {page}";

            var persons = _personRepository.FindWithPagedSearch(query);

            string countQuery = @"
                SELECT
                    COUNT(*)
                FROM
                    persons p
                WHERE
                    1=1 ";

            if (!string.IsNullOrEmpty(nome))
                countQuery += $" AND p.FirstName LIKE '%{nome}%' ";

            int totalResult = _personRepository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page + 1,
                List = _personConverter.Parse(persons),
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResult
            };
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _personConverter.Parse(_personRepository.FindByName(firstName, lastName));
        }
    }
}