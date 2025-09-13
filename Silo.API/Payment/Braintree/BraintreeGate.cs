using Braintree;
using Microsoft.Extensions.Options;

namespace Silo.API.Payment.Braintree;

public class BraintreeGate : IBraintreeGate
{
    public BraintreeSettings _braintreeSettings { get; }
    private IBraintreeGateway? braintreeGateway { get; set; }

    public BraintreeGate(IOptions<BraintreeSettings> braintreeSettings)
    {
        _braintreeSettings = braintreeSettings.Value;
    }

    public IBraintreeGateway CreateGateway()
    {
        braintreeGateway = new BraintreeGateway(_braintreeSettings.Environment, _braintreeSettings.MerchantId, _braintreeSettings.PublicKey, _braintreeSettings.PrivateKey);
        return braintreeGateway;
    }

    public IBraintreeGateway GetGateway()
    {
        if (braintreeGateway == null)
        {
            return CreateGateway();
        }
        return braintreeGateway;
    }
}