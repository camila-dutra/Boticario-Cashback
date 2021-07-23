using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cashback.Auth.Services;
using Cashback.Domain.DTOs;
using Cashback.Domain.Entities;
using Cashback.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Cashback.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        [HttpGet]
        [SwaggerResponse(200, Type = typeof(Purchase), Description = "Purchase list")]
        [SwaggerResponse(204, Type = typeof(string), Description = "No purchase found!")]
        public IActionResult GetPurchases([FromQuery]long cpf)
        {
            //string cpfStr = TokenService.GetValueFromClaim(HttpContext.User.Identity, ClaimTypes.NameIdentifier);
            //long.TryParse(cpfStr, out long cpf);
            return Ok(this.purchaseService.GetPurchases(cpf));
        }

        [HttpPost]
        [SwaggerResponse(200, Type = typeof(Purchase), Description = "Registered purchase")]
        [SwaggerResponse(204, Type = typeof(string), Description = "No purchase found!")]
        public IActionResult Post(PurchaseDTO purchase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(this.purchaseService.PostPurchase(purchase));
        }
    }
}
