using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonsApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //configure data context
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            AddTestData(app.ApplicationServices.GetService<DataContext>());
        }

        private void AddTestData(DataContext context)
        {
            var testPerson1 = new Person();
            testPerson1.Name = "Jonathan";
            context.Persons.Add(testPerson1);
            context.SaveChanges();

            var testPerson2 = new Person();
            testPerson2.Name = "Priscila";
            context.Persons.Add(testPerson2);
            context.SaveChanges();
        }
    }
}
