using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/users/v{version:apiVersion}")]
    public class UsersController : Controller
    {
        private IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody]UserVO user)
        {
            if (user == null) return BadRequest();
            return _userBusiness.FindByLogin(user);
        }
    }
}