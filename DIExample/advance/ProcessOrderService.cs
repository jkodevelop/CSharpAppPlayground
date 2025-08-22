using Microsoft.Extensions.Logging;
using System;

namespace CSharpAppPlayground.DIExample.advance
{
    public class ProcessOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly IShippingService _shippingService;
        private readonly ILogger<ProcessOrderApp> _logger;

        public ProcessOrderService(IPaymentService paymentService, IShippingService shippingService, ILogger<ProcessOrderApp> logger)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _shippingService = shippingService ?? throw new ArgumentNullException(nameof(shippingService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void ProcessOrder(string cardNumber, decimal amount)
        {
            // In real applications the credit card number should NEVER be logged or stored in plaintext.
            _logger.LogInformation("Processing order with card {CardNumber} for amount {Amount}", cardNumber, amount);
            // Process payment
            _paymentService.Charge(cardNumber, amount);
            // Process shipping
            _shippingService.Pay(cardNumber, amount);
            _logger.LogInformation("Order processed successfully for card {CardNumber}", cardNumber);
        }
    }
}
