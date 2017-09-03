namespace Todo.Common.Logging
{
    /// <summary>
    /// The static log factory for DI purpose.
    /// </summary>
    public static class LogFactory
    {
        #region Members

        private static ILoggerFactory _currentLogFactory;

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the current log factory to use.
        /// </summary>
        /// <param name="logFactory">Log factory to use.</param>
        public static void SetCurrent(ILoggerFactory logFactory)
        {
            _currentLogFactory = logFactory;
        }

        /// <summary>
        /// Gets the current log factory.
        /// </summary>
        /// <returns></returns>
        public static ILoggerFactory GetCurrent()
        {
            return _currentLogFactory;
        }

        /// <summary>
        /// Creates a new <see cref="ILogger"/> object.
        /// </summary>
        /// <returns>Created ILogger object.</returns>
        public static ILogger Create()
        {
            return _currentLogFactory?.Create();
        }

        /// <summary>
        /// Creates a new <see cref="ILogger"/> object with source name.
        /// </summary>
        /// <param name="source">The log source name.</param>
        /// <returns>Created ILogger</returns>
        public static ILogger Create(string source)
        {
            return _currentLogFactory?.Create(source);
        }

        #endregion
    }
}
