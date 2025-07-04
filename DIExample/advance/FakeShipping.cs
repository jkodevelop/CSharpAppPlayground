using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DIExample.advance
{
    public class FakeShipping: IShippingService
    {
        private readonly ICreditCardValidator _creditCardValidator;
        /// <summary>
        /// The reason ILogger<ProcessOrderApp> is because the IHost build creates the logger resources based on the class it is in.
        /// So to allow for automatic resolution of the ILogger dependency, we need to use the same class name as the one provided by IHost
        /// Alternatively, more ILogger types of be implemented to be passed for resolution in IHost build configuration.
        /// </summary>
        private readonly ILogger<ProcessOrderApp> _logger;
        public FakeShipping(ICreditCardValidator creditCardValidator, ILogger<ProcessOrderApp> logger)
        {
            _creditCardValidator = creditCardValidator ?? throw new ArgumentNullException(nameof(creditCardValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Pay(string cardNumber, decimal amount) 
        {
            // Simulate shipping payment processing
            if (_creditCardValidator.ValidateCardNumber(cardNumber) == false)
                _logger.LogError("Shipping: Invalid card number: {CardNumber}", cardNumber);
            else
                _logger.LogInformation("Shipping payment of {Amount} for card {CardNumber}", amount, cardNumber);
        }
    }
}
