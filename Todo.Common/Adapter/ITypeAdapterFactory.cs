using System;

namespace Todo.Common.Adapter
{
    /// <summary>
    /// Interface for adapter factory.
    /// </summary>
    public interface ITypeAdapterFactory
    {
        /// <summary>
        /// Create a <see cref="ITypeAdapter"/> object.
        /// </summary>
        /// <returns>The new <see cref="ITypeAdapter"/></returns>
        ITypeAdapter Create();

        /// <summary>
        /// Create a <see cref="ITypeAdapter"/> object with specified type mapping.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <returns>The new <see cref="ITypeAdapter"/> object.</returns>
        ITypeAdapter CreateWithMap<TSource, TTarget>();

        /// <summary>
        /// Create a <see cref="ITypeAdapter"/> object with specified type mapping.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="target">The target type.</param>
        /// <returns>The new <see cref="ITypeAdapter"/> object.</returns>
        ITypeAdapter CreateWithMap(Type source, Type target);
    }
}
