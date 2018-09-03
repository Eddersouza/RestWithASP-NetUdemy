using RestWithASPNetUdemy.Model;

namespace RestWithASPNetUdemy.Business
{
    public interface IUserBusiness
    {
        object FindByLogin(User user);
    }
}