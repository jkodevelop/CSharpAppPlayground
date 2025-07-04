using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DIExample.advance
{
    public class FakeCardValidator: ICreditCardValidator
    {
        public bool ValidateCardNumber(string cardNumber)
        {
            // Simple validation logic for demonstration purposes
            return !string.IsNullOrWhiteSpace(cardNumber) && cardNumber.Length == 16 && cardNumber.All(char.IsDigit);
        }
    }
}
