using System.Collections.Generic;

namespace Todo.Common.Adapter
{
    /// <summary>
    /// Extension methods for object mapping. You don't need to create map between 2 types in advance with these extensions.
    /// </summary>
    public static class AdapterExtensions
    {
        /// <summary>
        /// Project object to target type.
        /// </summary>
        /// <typeparam name="TSource">The source type</typeparam>
        /// <typeparam name="TTarget">The target type</typeparam>
        /// <param name="item">The source entity to be adapted.</param>
        /// <param name="target">The adapted target object.</param>
        /// <returns>The adapted target object.</returns>
        public static TTarget Adapt<TSource, TTarget>(this TSource item, TTarget target = null)
            where TSource : class 
            where TTarget : class
        {
            if (item == null) return null;

            var adapter = TypeAdapterFactory.CreateAdapter(typeof(TSource), typeof(TTarget));

            return target == null ? adapter.Adapt<TTarget>(item) : adapter.Adapt(item, target);
        }

        /// <summary>
        /// Project object to target type.
        /// </summary>
        /// <typeparam name="TTarget">The DTO type</typeparam>
        /// <param name="item">The source entity to adapt</param>
        /// <param name="target">The adapt target instance</param>
        /// <returns>The DTO</returns>
        public static TTarget AdaptAs<TTarget>(this object item, TTarget target = null)
            where TTarget : class
        {
            if (item == null) return null;

            var adapter = TypeAdapterFactory.CreateAdapter(item.GetType(), typeof(TTarget));

            return target == null ? adapter.Adapt<TTarget>(item) : adapter.Adapt(item, target);
        }

        /// <summary>
        /// Project a enumerable collection of items.
        /// </summary>
        /// <typeparam name="TSource">The source type</typeparam>
        /// /// <typeparam name="TTarget">The DTO type</typeparam>
        /// <param name="items">the collection of entity items</param>
        /// <returns>Projected DTO collection</returns>
        public static List<TTarget> AdaptAsList<TSource, TTarget>(this IEnumerable<TSource> items)
            where TTarget : class
        {
            if (items == null) return new List<TTarget>();

            var adapter = TypeAdapterFactory.CreateAdapter<TSource, TTarget>();

            return adapter.Adapt<List<TTarget>>(items);
        }
    }
}
