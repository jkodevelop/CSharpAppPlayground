using CSharpAppPlayground.DIExample.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAppPlayground.DIExample.Services
{
    public class TesterCaseBService : ITesterService
    {
        private readonly ILogger<TesterCaseBService> _logger;

        public TesterCaseBService(ILogger<TesterCaseBService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool RunTestA()
        {
            _logger.LogInformation("Running Test A");
            // Simulate test logic
            bool result = true; // Assume the test passes
            _logger.LogWarning($"Test A completed with result: {result}");
            return result;
        }

        public bool RunTestB()
        {
            _logger.LogInformation("Running Test B");
            // Simulate test logic
            bool result = false; // Assume the test fails
            _logger.LogWarning($"Test B completed with result: {result}");
            return result;
        }
    }
}
