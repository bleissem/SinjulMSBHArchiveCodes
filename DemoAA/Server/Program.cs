using System.Threading.Tasks;

using DemoAA.Server.Data;
using DemoAA.Server.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoAA.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (IHost host = CreateHostBuilder(args).Build())
            {
                using var scoped = host.Services.CreateScope();

                // await EnsureCrateDatabase(scoped);

                // await Initializing(scoped);

                await host.RunAsync();
            }

            static async Task EnsureCrateDatabase(IServiceScope scope)
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                _ = await context.Database.EnsureCreatedAsync();
            }

            static async Task Initializing(IServiceScope scope)
            {
                UserManager<ApplicationUser> um = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<IdentityRole> rm = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                ApplicationUser user = new()
                {
                    UserName = "Sinjul.MSBH@Yaoo.com",
                    Email = "Sinjul.MSBH@Yaoo.com",
                    EmailConfirmed = true,
                };

                ApplicationUser admin = new()
                {
                    UserName = "JackSlater.Irani@Gmail.com",
                    Email = "JackSlater.Irani@Gmail.com",
                    EmailConfirmed = true,
                };

                _ = await um.CreateAsync(user, "Pa$$w0rd");
                _ = await um.CreateAsync(admin, "Pa$$w0rd");

                _ = await rm.CreateAsync(new IdentityRole { Name = "user" });
                _ = await rm.CreateAsync(new IdentityRole { Name = "admin" });

                _ = await um.AddToRoleAsync(user, "user");
                _ = await um.AddToRoleAsync(admin, "admin");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    _ = webBuilder.UseStartup<Startup>();
                })
            ;
        }
    }
}
