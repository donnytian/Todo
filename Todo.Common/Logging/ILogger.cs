using System;

namespace Todo.Common.Logging
{
    /// <summary>
    /// Common contract for trace instrumentation.
    /// You can implement this contract with several frameworks.
    /// .NET Diagnostics API, EntLib, Log4Net,NLog etc.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log message information for debug purpose.
        /// </summary>
        /// <param name="message">The information message to write</param>
        /// <param name="args">The arguments values</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Log message information.
        /// </summary>
        /// <param name="message">The information message to write</param>
        /// <param name="args">The arguments values</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Log warning message.
        /// </summary>
        /// <param name="message">The warning message to write</param>
        /// <param name="args">The argument values</param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// Log error message.
        /// </summary>
        /// <param name="exception">The exception associated with this error</param>
        void Error(Exception exception);

        /// <summary>
        /// Log error message.
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="args">The arguments values</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Log error message.
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="exception">The exception associated with this error</param>
        /// <param name="args">The arguments values</param>
        void Error(string message, Exception exception, params object[] args);

        /// <summary>
        /// Log error message.
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="args">The arguments values</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// Log error message.
        /// </summary>
        /// <param name="message">The error message to write</param>
        /// <param name="exception">The exception associated with this error</param>
        /// <param name="args">The arguments values</param>
        void Fatal(string message, Exception exception, params object[] args);
    }
}
