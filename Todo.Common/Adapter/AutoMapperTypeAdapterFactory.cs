using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Todo.Common.Adapter
{
    /// <summary>
    /// AutoMapper implementation for <see cref="ITypeAdapterFactory"/> interface.
    /// </summary>
    public class AutoMapperTypeAdapterFactory : ITypeAdapterFactory
    {
        #region Members

        // The default mapper.
        private readonly IMapper _defaultMapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new AutoMapper type adapter factory.
        /// </summary>
        public AutoMapperTypeAdapterFactory()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Search all assemblies to observe AutoMapper Profile classes.
                var profiles = AppDomain.CurrentDomain
                                        .GetAssemblies()
                                        .SelectMany(SafeGetTypesForAssembly)
                                        .Where(t => t?.BaseType == typeof(Profile) && !t.FullName.StartsWith("AutoMapper."));

                foreach (var item in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });

            _defaultMapper = config.CreateMapper();
        }

        /// <summary>
        /// Create a new AutoMapper type adapter factory with specified configuration.
        /// </summary>
        public AutoMapperTypeAdapterFactory(MapperConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _defaultMapper = config.CreateMapper();
        }

        #endregion

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutoMapperTypeAdapter(_defaultMapper);
        }

        public ITypeAdapter CreateWithMap<TSource, TTarget>()
        {
            var map = _defaultMapper.ConfigurationProvider.FindTypeMapFor<TSource, TTarget>();
            var mapper = map != null
                ? _defaultMapper
                : new MapperConfiguration(cfg => cfg.CreateMap<TSource, TTarget>()).CreateMapper();

            return new AutoMapperTypeAdapter(mapper);
        }

        public ITypeAdapter CreateWithMap(Type source, Type target)
        {
            var map = _defaultMapper.ConfigurationProvider.FindTypeMapFor(source, target);
            var mapper = map != null
                ? _defaultMapper
                : new MapperConfiguration(cfg => cfg.CreateMap(source, target)).CreateMapper();
            
            return new AutoMapperTypeAdapter(mapper);
        }

        #endregion

        #region Private Methods

        private static Type[] SafeGetTypesForAssembly(Assembly assembly)
        {
            try
            {
                return assembly?.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types;
            }
        }

        #endregion
    }
}
