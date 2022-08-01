using Microsoft.AspNetCore.Mvc;
using Poc.Intercepeter.Api.Domain.Credit;

namespace Poc.Intercepeter.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CreditController : ControllerBase
    {
        private readonly ILogger<CreditController> _logger;
        private readonly ICreditDomain _creditDomain;

        public CreditController(ILogger<CreditController> logger, ICreditDomain creditDomain)
        {
            _logger = logger;
            _creditDomain = creditDomain;
        }

        [HttpPost(Name = "Post")]
        public async Task<ActionResult<bool>> PostAsync(CreditRequest creditRequest)
        {
            return await _creditDomain.ToApproveAsync(creditRequest);
        }
    }
}