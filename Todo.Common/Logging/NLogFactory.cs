using System;
using System.Diagnostics;

namespace Todo.Common.Logging
{
    /// <summary>
    /// A NLog implementation of <see cref="ILoggerFactory"/>.
    /// </summary>
    public class NLogFactory : ILoggerFactory
    {
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
            return new NLogger(source);
        }

        /// <inheritdoc />
        public ILogger Create<T>()
        {
            return new NLogger(typeof(T).FullName);
        }
    }
}
