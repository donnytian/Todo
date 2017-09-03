using System;
using System.Diagnostics;
using MsILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace Todo.Common.Logging
{
    /// <summary>
    /// A .NET Core implementation of <see cref="ILoggerFactory"/>.
    /// </summary>
    public class NetCoreLoggerFactory : ILoggerFactory
    {
        private readonly MsILoggerFactory _factory;

        public NetCoreLoggerFactory(MsILoggerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <inheritdoc />
        public ILogger Create()
        {
            string className;
            Type declaringType;
            var framesToSkip = 2; // Skipped: 1 - current method, 2 - LogFactory.Create() method.

            do
            {
                var frame = new StackFrame(framesToSkip, false);
                var method = frame.GetMethod();
                declaringType = method.DeclaringType;

                if (declaringType == null)
                {
                    className = method.Name;
                    break;
                }

                framesToSkip++;
                className = declaringType.FullName;
            } while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return Create(className);
        }

        /// <inheritdoc />
        public ILogger Create(string source)
        {
            return new NetCoreLogger(source, _factory);
        }

        /// <inheritdoc />
        public ILogger Create<T>()
        {
            return new NetCoreLogger(typeof(T).FullName, _factory);
        }
    }
}
