
using HealthCheckDemo.Service1.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

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

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthCheckDemo.Service1", Version = "v1" });
        });


        services.AddHealthChecks()
            .AddCheck<ResponseTimeHealthCheck>("Network Speed Test", null, new[] { "service" });
            //.AddSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            //"SELECT 1;",
            //"Pazartane_Test_Supports",
            ////HealthStatus.Healthy,
            //timeout: TimeSpan.FromSeconds(5),
            //tags: new[] { "db", "sql", "sqlserver" })

            //.AddRedis("ada");


        services.AddSingleton<ResponseTimeHealthCheck>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthCheckDemo.Service1 v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {

            //endpoints.MapHealthChecks("/quickhc", new HealthCheckOptions()
            //{
            //    Predicate = _ => false
            //});

            endpoints.MapHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapControllers();
        });
    }
}