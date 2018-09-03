using RestWithASPNetUdemy.Data.VO;

namespace RestWithASPNetUdemy.Business
{
    public interface IUserBusiness
    {
        object FindByLogin(UserVO user);
    }
}