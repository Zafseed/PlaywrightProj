namespace PlaywrightProj
{
    public class Logger
    {
        private readonly string _logFolderPath;

        public Logger(string path)
        {
            _logFolderPath = path;
        }

        public void Log(string message)
        {
            string logMessage = $"[{DateTime.Now}] {message}";

            File.AppendAllText(_logFolderPath, logMessage + Environment.NewLine);
        }
    }
}
