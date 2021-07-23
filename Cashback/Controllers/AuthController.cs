using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cashback.Domain.DTOs;
using Cashback.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Cashback.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("authenticate"), AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Authenticate(UserAuthenticateRequestDTO user)
        {
            return Ok(this.authService.Authenticate(user));
        }

    }
}
