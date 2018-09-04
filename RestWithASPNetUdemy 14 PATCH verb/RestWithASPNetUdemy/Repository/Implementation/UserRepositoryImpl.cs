using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Model.Context;
using System.Linq;

namespace RestWithASPNetUdemy.Repository.Implementation
{
    public class UserRepositoryImpl : IUserRepository
    {
        private MySQLContext _context;

        public UserRepositoryImpl(MySQLContext mySQLContext)
        {
            _context = mySQLContext;
        }

        public User FindByLogin(string login)
        {
            return _context.Users.SingleOrDefault(p => p.Login.Equals(login));
        }
    }
}