using Microsoft.Extensions.Logging;

namespace CSharpAppPlayground.DIExample.median
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
