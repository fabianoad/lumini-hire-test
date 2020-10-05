using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaAutentica.Areas.Identity.Data;
using SistemaAutentica.Data;

[assembly: HostingStartup(typeof(SistemaAutentica.Areas.Identity.IdentityHostingStartup))]
namespace SistemaAutentica.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SistemaAutenticaDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SistemaAutenticaDBContextConnection")));

                services.AddDefaultIdentity<SistemaAutenticaUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SistemaAutenticaDBContext>();
            });
        }
    }
}