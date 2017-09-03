using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Todo.Common.Logging;

namespace Todo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            // Note: make sure the current factory is set by LogFactory.SetCurrent during Startup.
            LogFactory.GetCurrent().Create<Program>().Info("Application Shutdown.");
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
