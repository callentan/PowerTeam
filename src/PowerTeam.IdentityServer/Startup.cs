using System.Linq;
using System.Reflection;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerTeam.IdentityServer;
using PowerTeam.IdentityServer.ConfigurationStore;

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
            services.AddDbContext<ConfigurationStoreContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("ConfigurationStoreConnection"),
                b => b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name)
                )
            );

            services.AddTransient<IClientStore, ClientStore>();
            services.AddTransient<IResourceStore, ResourceStore>();

            services.AddIdentityServer()
                .AddResourceStore<ResourceStore>()
                .AddClientStore<ClientStore>();
                //.AddAspNetIdentity<ApplicationUser>()
                //.AddProfileService<IdentityWithAdditionalClaimsProfileService>();

            //InMemoryConfiguration.Configuration = this.Configuration;

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddTestUsers(InMemoryConfiguration.GetUsers().ToList())
            //    .AddInMemoryClients(InMemoryConfiguration.GetClients())
            //    .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());

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
