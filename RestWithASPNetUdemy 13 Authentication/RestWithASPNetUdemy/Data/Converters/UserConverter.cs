using RestWithASPNetUdemy.Data.Converter;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNetUdemy.Data.Converters
{
    public class UserConverter : IParser<UserVO, User>, IParser<User, UserVO>
    {
        public UserVO Parse(User origin)
        {
            if (origin == null) return new UserVO();

            return new UserVO
            {
                AcessKey = origin.AcessKey,
                Login = origin.Login
            };
        }

        public List<UserVO> Parse(List<User> origin)
        {
            if (origin == null) return new List<UserVO>();

            return origin.Select(item => Parse(item)).ToList();
        }

        public User Parse(UserVO origin)
        {
            if (origin == null) return new User();

            return new User
            {
                AcessKey = origin.AcessKey,
                Login = origin.Login
            };
        }

        public List<User> Parse(List<UserVO> origin)
        {
            if (origin == null) return new List<User>();

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}