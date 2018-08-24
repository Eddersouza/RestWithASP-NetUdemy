using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNetUdemy.Controllers
{
    [Produces("application/json")]
    [Route("api/Persons")]
    public class PersonsController : Controller
    {
    }
}