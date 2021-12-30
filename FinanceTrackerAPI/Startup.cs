using AutoMapper;
using FinanceTrackerAPI.Data;
using FinancialTracker.Common;
using FinancialTracker.Core.Lib;
using FinancialTracker.Core.Lib.CoreServices;
using FinancialTracker.Models.Options;
using FinancialTracker.Services;
using FinancialTracker.Services.CoreServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Identity.Web;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace FinanceTrackerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<IInvestmentRepository, InvestmentContext>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<BaseContext>(options =>
                 options.UseSqlServer(Configuration.GetValue<string>("DefaultConnectionAAD")));

            services.Configure<AppInsightsOptions>(Configuration.GetSection("ApplicationInsights"));
            services.AddDistributedMemoryCache();
            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration, "AuthSettings");

            services.AddMemoryCache();

            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile(new AutoMapperProfiles())
            );

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors();

            services.AddLogging(options =>
            {
                options.AddConsole();
                options.SetMinimumLevel(LogLevel.Trace);
                options.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
                options.AddApplicationInsights(Configuration["ApplicationInsights:InstrumentationKey"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinanceTracker API");
            });

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
