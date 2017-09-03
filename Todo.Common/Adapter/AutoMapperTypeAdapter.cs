using System;
using AutoMapper;

namespace Todo.Common.Adapter
{
    /// <summary>
    /// AutoMapper type adapter implementation.
    /// </summary>
    public class AutoMapperTypeAdapter : ITypeAdapter
    {
        #region Members

        // The internal mapper.
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize a new instance of <see cref="AutoMapperTypeAdapter"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public AutoMapperTypeAdapter(IMapper mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            _mapper = mapper;
        }

        #endregion

        #region ITypeAdapter Members

        public TTarget Adapt<TTarget>(object source) where TTarget : class
        {
            return _mapper.Map<TTarget>(source);
        }

        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class
        {
            return _mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Adapt<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return _mapper.Map(source, target);
        }

        #endregion
    }
}
