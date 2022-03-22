using KhareedLo.Models;
using KhareedLo.Repositories;
using KhareedLo.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KhareedLo.ViewModel;
using System.Text.Json.Serialization;
using KhareedLo.Auth;
using Newtonsoft.Json;

namespace KhareedLo
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

           // services.AddAuthentication().AddGoogle(options =>{
           //IConfigurationSection googleAuthNSection =
           //     Configuration.GetSection("Authentication:Google");
           //     options.ClientId = googleAuthNSection["ClientID"];
           //     options.ClientSecret = googleAuthNSection["ClientSecret"];
           //});

            services.AddAuthentication().AddGoogle(options => {

                options.SignInScheme = IdentityConstants.ExternalScheme;
                options.ClientId = "993170839779-406bp87todv8ik8n1svu65eigsi1o1r8.apps.googleusercontent.com";
                options.ClientSecret = "9Tdr7LFcwhXUbm7-LlAOEWNa";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IFeedbackRepository, FeedbackRepository>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();

            //services.AddTransient<IGenericRepository, GenericRepository><>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient(typeof(Interface<>), typeof(Repository<>));

            services.AddScoped<SignInManager<LoginViewModel>>();

            services.AddControllers().AddNewtonsoftJson();

            //services.AddIdentity<IdentityUser, IdentityRole>()    
            //    .AddEntityFrameworkStores<AppDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            }
            ).AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteProduct", policy => policy.RequireClaim("DeleteProduct"));
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //var json = JsonConvert.SerializeObject(harry,
            //        new JsonSerializerSettings()
            //        {
            //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
           
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
