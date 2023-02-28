using Microsoft.Extensions.Configuration;

namespace Intuit.Negocio
{
    public static class EFStringConnection
    {
        public static IConfiguration Configuration { get; set; }
        public static string StringConnection { get; set; }

        public static string GetStringConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            string connectionStringUsed = Configuration["ConnectionStringUsed"];
            StringConnection = Configuration.GetConnectionString(connectionStringUsed);

            return StringConnection!;
        }
    }
}
