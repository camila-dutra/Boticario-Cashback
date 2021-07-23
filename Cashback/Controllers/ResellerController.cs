using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cashback.Domain.DTOs;
using Cashback.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Cashback.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResellerController : ControllerBase
    {
        private readonly IResellerService resellerService;

        public ResellerController(IResellerService resellerService)
        {
            this.resellerService = resellerService;
        }

        [HttpPost]
        public IActionResult Post(ResellerRequestDTO reseller)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(this.resellerService.PostReseller(reseller));
        }

        [HttpGet("{cpf}/cashback")]
        public IActionResult GetResellerCashback(long cpf)
        {
            try
            {
                var result = this.resellerService.GetResellerCashback(cpf);
                return Ok(new { data = result });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
