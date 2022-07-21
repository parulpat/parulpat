using LibraryManagement.IServices;
using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement
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
            services.AddControllersWithViews();
            services.AddScoped<IRegisterUser, RegisterUserService>();
            services.AddScoped<CommonServices>();
            services.AddScoped<IBook, BookService>();
            services.AddScoped<ICategory, CategoryService>();
            services.AddScoped<IAuthor, AuthorService>();
            services.AddScoped<IStudent, StudentService>();
            services.AddSession(option => {
                // default session time out is 20 minutes 
                // but we can set it to any time span 
                option.IdleTimeout = TimeSpan.FromMinutes(30);

                // allows to use the session cookie 
                // even if the user hasn't consented 
                option.Cookie.IsEssential = true;
            });
            services.AddDbContext<LibraryManagementDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connection")));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
