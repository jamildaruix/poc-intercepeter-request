namespace Poc.Intercepeter.Api.Domain.Credit
{
    public interface ICreditDomain
    {
        Task<bool> ToApproveAsync(CreditRequest creditRequest);
    }
}
