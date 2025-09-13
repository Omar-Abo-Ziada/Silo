using Braintree;

namespace Silo.API.Payment.Braintree;

public interface IBraintreeGate
{
    IBraintreeGateway CreateGateway();

    IBraintreeGateway GetGateway();
}