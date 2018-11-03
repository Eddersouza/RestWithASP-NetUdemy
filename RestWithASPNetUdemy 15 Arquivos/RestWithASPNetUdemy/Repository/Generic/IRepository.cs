using RestWithASPNetUdemy.Model.Base;
using System.Collections.Generic;

namespace RestWithASPNetUdemy.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);

        void Delete(long id);

        bool Exists(long id);

        List<T> FindAll();

        T FindById(long id);

        List<T> FindWithPagedSearch(string query);

        int GetCount(string query);

        T Update(T item);
    }
}