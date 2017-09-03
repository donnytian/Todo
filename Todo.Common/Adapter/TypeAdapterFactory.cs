using System;

namespace Todo.Common.Adapter
{
    /// <summary>
    /// The static factory for DI purpose.
    /// </summary>
    public static class TypeAdapterFactory
    {
        #region Members

        // The current type adapter factory.
        private static ITypeAdapterFactory _currentTypeAdapterFactory;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Set the current type adapter factory.
        /// </summary>
        /// <param name="adapterFactory">The adapter factory to set</param>
        public static void SetCurrent(ITypeAdapterFactory adapterFactory)
        {
            _currentTypeAdapterFactory = adapterFactory;
        }

        /// <summary>
        /// Create a new type adapter from current factory based on default mappings.
        /// </summary>
        /// <returns>Created type adapter</returns>
        public static ITypeAdapter CreateAdapter()
        {
            return _currentTypeAdapterFactory?.Create();
        }

        /// <summary>
        /// Create a new type adapter to include the specified mapping.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <returns>The new <see cref="ITypeAdapter"/> object.</returns>
        /// <returns>The new <see cref="ITypeAdapter"/> object.</returns>
        public static ITypeAdapter CreateAdapter<TSource, TTarget>()
        {
            return _currentTypeAdapterFactory?.CreateWithMap<TSource, TTarget>();
        }

        /// <summary>
        /// Create a new type adapter to include the specified mapping.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <param name="target">The target type.</param>
        /// <returns>The new <see cref="ITypeAdapter"/> object.</returns>
        public static ITypeAdapter CreateAdapter(Type source, Type target)
        {
            return _currentTypeAdapterFactory?.CreateWithMap(source, target);
        }

        #endregion
    }
}
