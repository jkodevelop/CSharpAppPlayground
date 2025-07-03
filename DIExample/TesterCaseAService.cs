using CSharpAppPlayground.DIExample.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DIExample.Services
{
    public class TesterCaseAService : ITesterService
    {
        private readonly ILogger<TesterCaseAService> _logger;

        public TesterCaseAService(ILogger<TesterCaseAService> logger)
        {
            _logger = logger;
        }

        public bool RunTestA()
        {
            _logger.LogInformation("Running Test A in TesterCaseAService.");
            return true;
        }

        public bool RunTestB()
        {
            _logger.LogInformation("Running Test B in TesterCaseAService.");
            return true;
        }
    }
}
