using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SeedData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();

                var configuration = new ConfigurationBuilder()
                    .AddJsonFile($"{currentDirectory}\\..\\AspNetCoreIdentityServer4\\appsettings.json")
                    .Build();

                var configurationStoreConnection = configuration.GetConnectionString("ConfigurationStoreConnection");

                var optionsBuilder = new DbContextOptionsBuilder<ConfigurationStoreContext>();
                optionsBuilder.UseSqlite(configurationStoreConnection);

                using (var configurationStoreContext = new ConfigurationStoreContext(optionsBuilder.Options))
                {
                    configurationStoreContext.AddRange(Config.GetClients());
                    configurationStoreContext.AddRange(Config.GetIdentityResources());
                    configurationStoreContext.AddRange(Config.GetApiResources());
                    configurationStoreContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
