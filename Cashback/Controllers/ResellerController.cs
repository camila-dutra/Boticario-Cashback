using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;
using Cashback.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Cashback.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ResellerController : ControllerBase
    {
        private readonly IResellerService resellerService;

        public ResellerController(IResellerService resellerService)
        {
            this.resellerService = resellerService;
        }

        [HttpPost, AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post(ResellerRequestDTO reseller)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(this.resellerService.PostReseller(reseller));
        }

        [HttpGet("{cpf}/cashback")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetResellerCashback(long cpf)
        {
            try
            {
                var result = await this.resellerService.GetResellerCashback(cpf);
                return Ok(new { data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
