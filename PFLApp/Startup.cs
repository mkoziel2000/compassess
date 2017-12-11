using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PFLApp.Data;
using PFLApp.Models;
using PFLApp.Services;
using PFLApp.Configuration;
using Microsoft.Extensions.Logging;

namespace PFLApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=pfl.db"));

            services.AddDbContext<PflCustomerDbContext>(options =>
                options.UseSqlite("Data Source=pfl-customer.db"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add Configuration to Pipeline
            services.Configure<PflLinkConfig>(Configuration.GetSection(nameof(PflLinkConfig)));
            services.Configure<X509CertConfig>(Configuration.GetSection(nameof(X509CertConfig)));

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IPflApiSvc, PflApiSvc>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            LoggerFactory factory = new LoggerFactory();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
