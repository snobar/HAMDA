namespace HAMDA.Provider.Middleware
{
    public class FileLoggerService
    {
        private readonly string _logFilePath;

        public FileLoggerService(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public async Task LogErrorAsync(string message)
        {
            try
            {
                // Ensure the directory exists
                var directory = Path.GetDirectoryName(_logFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Append the error message to the log file
                await File.AppendAllTextAsync(_logFilePath, $"{DateTime.UtcNow}: {message}\n");
            }
            catch (Exception ex)
            {
                // Handle any exceptions related to file I/O
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }
}
