using Microsoft.AspNetCore.Mvc;
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
        protected IActionResult HandleResponse<T>(Result<T> result)
        {
            if (result.Succedded)
            {
                return Ok(result.Instance);
            }

            HttpStatusCode status = (HttpStatusCode)result.Status;

            if (status == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            return BadRequest(result.Message);
        }
    }
}
