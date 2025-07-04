using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DIExample.advance
{
    public interface ICreditCardValidator
    {
        bool ValidateCardNumber(string cardNumber);
    }
}
