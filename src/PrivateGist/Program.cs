using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PrivateGist
{
#pragma warning disable RCS1102 // Make class static.
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
#pragma warning restore RCS1102 // Make class static.
}
