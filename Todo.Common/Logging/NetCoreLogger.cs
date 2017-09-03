using System;
using Microsoft.Extensions.Logging;
using MsILogger = Microsoft.Extensions.Logging.ILogger;
using MsILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace Todo.Common.Logging
{
    /// <summary>
    /// Represents a logger implemented by MS .NET Core to be used for logging.
    /// </summary>
    public class NetCoreLogger : ILogger
    {
        #region Members

        // The internal logger will be delegated with actual log activities.
        private readonly MsILogger _internalLogger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize a new instance of Logger.
        /// </summary>
        /// <param name="source">The source name of the log.</param>
        /// <param name="factory">The logger factory.</param>
        internal NetCoreLogger(string source, MsILoggerFactory factory)
        {
            _internalLogger = factory.CreateLogger(source);
        }

        #endregion

        #region ILogger Members

        public void Debug(string message, params object[] args)
        {
            _internalLogger.LogDebug(message, args);
        }

        public void Info(string message, params object[] args)
        {
            _internalLogger.LogInformation(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            _internalLogger.LogWarning(message, args);
        }

        public void Error(Exception exception)
        {
            _internalLogger.LogError(string.Empty, exception);
        }

        public void Error(string message, params object[] args)
        {
            _internalLogger.LogError(message, args);
        }

        public void Error(string message, Exception exception, params object[] args)
        {
            _internalLogger.LogError(exception, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            _internalLogger.LogCritical(message, args);
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            _internalLogger.LogCritical(exception, message, args);
        }

        #endregion
    }
}
