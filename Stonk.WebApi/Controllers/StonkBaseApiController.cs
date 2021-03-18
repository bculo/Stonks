using Microsoft.AspNetCore.Mvc;
using Stonk.Application.Contracts;
using Stonk.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stonk.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class StonkBaseApiController : ControllerBase
    {
        protected IActionResult HandleResponse(IResult result)
        {
            return Ok(result.GetResult());
        }
    }
}
