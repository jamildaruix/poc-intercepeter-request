namespace Poc.Intercepeter.Api.Domain.Credit;

public interface IApproveCreditService
{
    Task<bool> InvokeAsync(CreditRequest creditRequest);
}
