using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AspNet.Identity.MongoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Todo.Application;
using Todo.Common.Adapter;
using Todo.Common.Logging;
using Todo.Core;
using Todo.Mongo;
using Todo.Mongo.Repositories;
using Todo.Web.Security;

namespace Todo.Web
{
    /// <summary>
    /// Injects all application dependencies.
    /// </summary>
    public static class TodoServiceConfig
    {
        public static IServiceCollection AddTodoServices(this IServiceCollection services, IConfiguration config)
        {
            //
            // Configuration.
            //
            services.AddOptions();
            services.Configure<JwtOptions>(config.GetSection("JwtSecurityToken"));

            //
            // Object mapper.
            //
            var adapterFactory = new AutoMapperTypeAdapterFactory();
            TypeAdapterFactory.SetCurrent(adapterFactory);
            services.AddSingleton<ITypeAdapterFactory>(adapterFactory);

            //
            // Logging.
            //
            services.AddSingleton(LogFactory.GetCurrent());

            //
            // Security.
            // We use token (JWT) based authentication here.
            // For a better understanding regarding token authentication, see below page.
            // https://stormpath.com/blog/token-authentication-asp-net-core
            //
            var jwtOptions = config.GetSection("JwtSecurityToken").Get<JwtOptions>();
            var tokenValidationParameters = new TokenValidationParameters
            {
                // Check if the token is issued by us.
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                // Check if the token is expired.
                ValidateLifetime = true,
                // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time 
                // when validating the lifetime. As we're creating the tokens locally and validating them on the same 
                // machines which should have synchronized time, this can be set to zero. and default value will be 5minutes
                ClockSkew = TimeSpan.Zero,
            };

            //services.AddDbContext<SecurityDbContext>(options =>
            //    options.UseSqlServer(config.GetConnectionString("Default"), sqlOptions =>
            //        sqlOptions.MigrationsAssembly("Todo.EntityFrameworkCore")
            //    )
            //);

            // For demo purpose, we will show two ways to store the token: cookie or HTTP Authorization Header.
            // We will use these two authentication schemes on different Controllers.

            // Note: AddIdentity() will enable cookie as default authentication scheme.
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                //.AddEntityFrameworkStores<SecurityDbContext>() // Switches between EF and MongoDB with below line.
                .AddMongoDbStores(options => options.ConnectionString = config.GetConnectionString("Mongo"))
                .AddDefaultTokenProviders();

            // Since AddIdentity() has already added cookies, here we only change some settings.
            // Check below page for details on how to add cookie authentication w/ or w/o Identity framework.
            // https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x
            services.ConfigureApplicationCookie(options =>
            {
                // Prevents cookies from client script access. So we are safe for XSS attack.
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = config["Cookies:AccessTokenName"];
                // Tells system how to verify.
                options.TicketDataFormat = new JwtSecureDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters);
            });


            // Adds token validation for HTTP Authorization Header.
            // Use [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] on controllers/ actions
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // Todo: remove this line in production.
                    options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });
            // Turn off Microsoft's JWT handler that maps claim types to .NET's long claim type names
            // See more details at https://github.com/IdentityServer/IdentityServer3.Samples/issues/173
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //
            // Unit of Work.
            //
            services.AddScoped<IUserIdProvider, AspNetUserIdProvider>();

            //services.AddDbContext<TodoDbContext>(options =>
            //    options.UseSqlServer(config.GetConnectionString("Default"), sqlOptions =>
            //        sqlOptions.MigrationsAssembly("Todo.EntityFrameworkCore")
            //    )
            //);
            //services.AddScoped<IUnitOfWork, TodoDbContext>();
            services.AddScoped<IUnitOfWork, MongoDbUow>(sp => new MongoDbUow(config.GetConnectionString("Mongo"), sp.GetService<IUserIdProvider>()));
            services.AddScoped(sp => new MongoDbUow(config.GetConnectionString("Mongo"), sp.GetService<IUserIdProvider>()));

            //
            // Repositories.
            //
            //services.AddScoped<IRepository<TodoItem, IUnitOfWork>, EfRepository<TodoItem, TodoDbContext>>();
            services.AddScoped<IRepository<TodoItem, IUnitOfWork>, TodoItemRepository>();

            //
            // Application services
            //
            services.AddTransient<ITodoAppService, TodoAppService>();

            return services;
        }
    }
}
