using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerTeam.IdentityServer;

namespace PowerTeam
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
            //const string connectionString = @"Server=webnetqasqlawp1;Initial Catalog=powerteam;Persist Security Info=False;User ID=sa;Password=ASP+Rocks4U;Connection Timeout=30;";
            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            InMemoryConfiguration.Configuration = this.Configuration;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(InMemoryConfiguration.GetUsers().ToList())
                .AddInMemoryClients(InMemoryConfiguration.GetClients())
                .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
