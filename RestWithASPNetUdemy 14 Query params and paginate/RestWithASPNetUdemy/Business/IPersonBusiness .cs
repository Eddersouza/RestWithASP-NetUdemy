using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestWithASPNetUdemy.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindAll();

        List<PersonVO> FindByName(string firstName, string lastName);

        PagedSearchDTO<PersonVO> FindWithPagedSearch(string nome, string sortDirection, int pageSize, int page);

        PersonVO Update(PersonVO person);

        void Delete(long id);
    }
}