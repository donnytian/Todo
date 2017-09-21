using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Todo.Common.Logging;
using Todo.Web.Security;
using MsILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace Todo.Web
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, MsILoggerFactory loggerFactory)
        {
            Configuration = configuration;

            LogFactory.SetCurrent(new NetCoreLoggerFactory(loggerFactory));
            _logger = LogFactory.GetCurrent().Create<Startup>();
            _logger.Info("Application Startup.");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.Info("Start configure services.");

            var jwtOptions = Configuration.GetSection("JwtSecurityToken").Get<JwtOptions>();
            services.AddMvc(options =>
            {
                // Adds global filters.
                options.Filters.Add(new AuthorizeFilter(string.Empty));

                if (jwtOptions.UseCookie)
                {
                    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                }
            });

            // Adds application specific services.
            services.AddTodoServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.Info("Start configure pipeline.");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseTodo();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
