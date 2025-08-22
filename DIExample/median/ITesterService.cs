

namespace CSharpAppPlayground.DIExample.median
{
    public interface ITesterService
    {
        /// <summary>
        /// Runs the test and returns success or failure.
        /// </summary>
        /// <returns>bool: was the test successful or not</returns>
        bool RunTestA();
        bool RunTestB();
    }
}
