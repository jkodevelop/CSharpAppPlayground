

namespace CSharpAppPlayground.DIExample.medianB
{
    internal class DocReportSummary: IReportSummary
    {
        private readonly string _filePath;

        public DocReportSummary(string filePath = "report.txt")
        {
            _filePath = filePath;
        }

        public string GenerateSummary(string[] data)
        {
            // Example implementation that generates a summary from the data array
            // return $"Document Summary: {string.Join(", ", data)}";
            return $"{_filePath} Document Summary: {data.Length}\n";
        }
    }
}
