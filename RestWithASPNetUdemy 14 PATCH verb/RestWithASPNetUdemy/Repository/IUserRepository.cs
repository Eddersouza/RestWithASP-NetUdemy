using RestWithASPNetUdemy.Model;

namespace RestWithASPNetUdemy.Repository
{
    public interface IUserRepository
    {
        User FindByLogin(string login);
    }
}