

namespace CSharpAppPlayground.DIExample.advance
{
    public interface IPaymentService
    {
        void Charge(string cardNumber, decimal amount);
    }
}
