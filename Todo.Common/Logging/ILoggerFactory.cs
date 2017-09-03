namespace Todo.Common.Logging
{
    /// <summary>
    /// Base contract for <see cref="ILogger"/> abstract factory.
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Create a new <see cref="ILogger"/> object.
        /// </summary>
        /// <returns>The ILogger object created.</returns>
        ILogger Create();

        /// <summary>
        /// Create a new <see cref="ILogger"/> object with a source name.
        /// </summary>
        /// <param name="source">The log source name.</param>
        /// <returns>The ILogger object created.</returns>
        ILogger Create(string source);

        /// <summary>
        /// Create a new <see cref="ILogger"/> object with a type parameter.
        /// </summary>
        /// <typeparam name="T">The type that will actually use the looger.</typeparam>
        /// <returns>The ILogger object created.</returns>
        ILogger Create<T>();
    }
}
