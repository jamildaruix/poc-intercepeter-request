using Poc.Intercepeter.Api.Domain.Enum;

namespace Poc.Intercepeter.Api.Domain.Credit;

public record CreditStatusResponse(int IdTransation, StatusCreditEnum StatusCreditEnum, DateTime? DateTransaction);
