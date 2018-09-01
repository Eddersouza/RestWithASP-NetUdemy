using RestWithASPNetUdemy.Data.Converter;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNetUdemy.Data.Converters
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public PersonVO Parse(Person origin)
        {
            if (origin == null) return new PersonVO();

            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.LastName,
                Gender = origin.Gender
            };
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return new List<PersonVO>();

            return origin.Select(item => Parse(item)).ToList();
        }

        public Person Parse(PersonVO origin)
        {
            if (origin == null) return new Person();

            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.LastName,
                Gender = origin.Gender
            };
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return new List<Person>();

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}