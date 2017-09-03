using System;
using NLog;

namespace Todo.Common.Logging
{
    /// <summary>
    /// Represents a logger implemented by NLog to be used for logging.
    /// </summary>
    public class NLogger : ILogger
    {
        #region Members

        // The internal logger will be delegated with actual log activities.
        private readonly Logger _internalLogger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize a new instance of Logger.
        /// </summary>
        /// <param name="source">The source name of the log.</param>
        internal NLogger(string source)
        {
            _internalLogger = LogManager.GetLogger(source);
        }

        #endregion

        #region ILogger Members

        public void Debug(string message, params object[] args)
        {
            _internalLogger.Debug(message, args);
        }

        public void Info(string message, params object[] args)
        {
            _internalLogger.Info(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            _internalLogger.Warn(message, args);
        }

        public void Error(Exception exception)
        {
            _internalLogger.Error(exception);
        }

        public void Error(string message, params object[] args)
        {
            _internalLogger.Error(message, args);
        }

        public void Error(string message, Exception exception, params object[] args)
        {
            _internalLogger.Error(exception, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            _internalLogger.Fatal(message, args);
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            _internalLogger.Fatal(exception, message, args);
        }

        #endregion
    }
}
