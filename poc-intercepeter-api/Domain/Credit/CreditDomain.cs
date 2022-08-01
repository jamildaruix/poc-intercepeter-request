namespace Poc.Intercepeter.Api.Domain.Credit
{
    public class CreditDomain : ICreditDomain
    {
        public readonly IApproveCreditService _approveCreditService;

        public CreditDomain(IApproveCreditService approveCreditService) => _approveCreditService = approveCreditService;

        public async Task<bool> ToApproveAsync(CreditRequest creditRequest) => await _approveCreditService.InvokeAsync(creditRequest);
    }
}
