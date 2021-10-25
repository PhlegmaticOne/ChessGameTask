namespace ChessGameTask.Lib.Loggers
{
    /// <summary>
    /// Represents base class for any logger
    /// </summary>
    /// <typeparam name="T">Type that logger will log</typeparam>
    public abstract class Logger<T> where T: class
    {
        /// <summary>
        /// Abstract method that specifies how every logger will log specified entity
        /// </summary>
        /// <param name="entity">Entity for logging</param>
        public abstract void Log(T entity);
    }
}
