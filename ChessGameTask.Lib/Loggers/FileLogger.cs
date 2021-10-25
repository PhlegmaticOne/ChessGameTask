using System.IO;

namespace ChessGameTask.Lib.Loggers
{
    /// <summary>
    /// Represents logger that logs results into a text file
    /// </summary>
    /// <typeparam name="T">Log result type</typeparam>
    public class FileLogger<T> : Logger<T> where T: class
    {
        /// <summary>
        /// Path to file for logging results
        /// </summary>
        private readonly string _filePath;
        /// <summary>
        /// Initializes new logger instance with path to file where results will be logged
        /// </summary>
        public FileLogger(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
            }
            _filePath = filePath;
        }
        /// <summary>
        /// Logs entity into a file
        /// </summary>
        /// <param name="entity">Specified entity for logging</param>
        public override void Log(T entity)
        {
            using (var stream = File.AppendText(_filePath))
            {
                stream.WriteLine(entity?.ToString());
            }
        }
    }
}
