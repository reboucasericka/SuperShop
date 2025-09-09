using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperShop.Data;
using SuperShop.Data.Entities;
using SuperShop.Helpers;

namespace SuperShop
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

            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true; // Ensure unique email addresses for users
                cfg.Password.RequireDigit = false; // Require at least one digit in passwords mudar depois para true
                cfg.Password.RequiredUniqueChars = 0; // No unique characters required in passwords
                cfg.Password.RequireUppercase = false; // No uppercase letters required in passwords
                cfg.Password.RequireLowercase = false; // No lowercase letters required in passwords
                cfg.Password.RequireNonAlphanumeric = false; // No special characters required in passwords
                cfg.Password.RequiredLength = 6; // Minimum length of 6 characters for passwords
			})
            .AddEntityFrameworkStores<DataContext>();// Use DataContext for storing user data

			services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<SeedDb>();   // Register the SeedDb service to seed the database
			services.AddScoped<IUserHelper, UserHelper>(); // Register the IUserHelper service for user management
			services.AddScoped<IImageHelper, ImageHelper>();
			services.AddScoped<IConverterHelper, ConverterHelper>();
			//services.AddScoped<IRepository, Repository>(); // Register the repository service
			//services.AddScoped<IRepository, MockRepository>();
			services.AddScoped<IProductRepository, ProductRepository>(); 
			services.AddControllersWithViews(); // Add services for controllers with views
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
            app.UseAuthentication(); // Enable authentication middleware
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
