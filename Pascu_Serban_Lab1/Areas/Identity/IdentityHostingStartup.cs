﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pascu_Serban_Lab1.Data;

[assembly: HostingStartup(typeof(Pascu_Serban_Lab1.Areas.Identity.IdentityHostingStartup))]
namespace Pascu_Serban_Lab1.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddIdentity<IdentityUser, IdentityRole>(options =>
                    options.SignIn.RequireConfirmedAccount = true)
                     .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}