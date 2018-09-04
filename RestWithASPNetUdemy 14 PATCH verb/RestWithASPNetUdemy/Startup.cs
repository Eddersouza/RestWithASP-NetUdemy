using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Business.Implementation;
using RestWithASPNetUdemy.HyperMedia;
using RestWithASPNetUdemy.Model.Context;
using RestWithASPNetUdemy.Repository;
using RestWithASPNetUdemy.Repository.Generic;
using RestWithASPNetUdemy.Repository.Implementation;
using RestWithASPNetUdemy.Security.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using Tapioca.HATEOAS;

namespace RestWithASPNetUdemy
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; set; }

        private readonly ILogger _logger;

        public Startup(
            IConfiguration configuration,
            IHostingEnvironment environment,
            ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MysqlConnection:MysqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            ExecuteMigrations(connectionString);

            var signingConfigurations = new SigningConfiguration();
            services.AddSingleton(signingConfigurations);

            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(_configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfiguration);

            services.AddSingleton(tokenConfiguration);

            services.AddAuthentication(authOptions => {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;

                paramsValidation.ValidateIssuerSigningKey = true;

                paramsValidation.ValidateLifetime = true;

                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth=> {
                auth.AddPolicy(
                    "Bearer", 
                    new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlDataContractSerializerFormatters();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);

            services.AddApiVersioning(option => option.ReportApiVersions = true);

            ConfigureSwagger(services);

            ConfigureDependencyInjection(services);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new Info
                    {
                        Title = "RESTFull API With ASP.NET Core 2.0",
                        Version = "v1"
                    });
            });
        }

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IBookBusiness, BookBusinessImpl>();
            services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
            services.AddScoped<IUserBusiness, UserBusinessImpl>();
            services.AddScoped<IUserRepository, UserRepositoryImpl>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        private void ExecuteMigrations(string connectionString)
        {
            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed", ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","My API v1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}");
            });
        }
    }
}