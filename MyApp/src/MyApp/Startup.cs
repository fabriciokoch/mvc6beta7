using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Hosting;
using MyApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.AspNet.Http;

namespace MyApp {
  public class Startup {

    public static IConfiguration Configuration { get; set; }

    public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv) {
      var configurationBuilder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
         .AddJsonFile("config.json")
         .AddEnvironmentVariables();
      Configuration = configurationBuilder.Build();
    }

    public void ConfigureServices(IServiceCollection services) {
      services.AddMvc();
      services.AddEntityFramework().AddSqlServer().AddDbContext<MyAppDbContext>();
      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<MyAppDbContext>();
    }

    public void Configure(IApplicationBuilder app) {
      app.UseStaticFiles();
      app.UseIdentity();
      app.UseCookieAuthentication(options =>
      {
        options.LoginPath = new PathString("/account/login");
        options.AutomaticAuthentication = true;
        options.AuthenticationScheme = "Cookies";
      });
      app.UseMvc(routes => {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
      });
      CreateUser(app.ApplicationServices).Wait();
    }

    private static async Task CreateUser(IServiceProvider applicationServices) {
      using (var c = (MyAppDbContext)applicationServices.GetService(typeof(MyAppDbContext))) {
        c.Database.EnsureCreated();
        c.Database.Migrate();
        int users = await c.Users.CountAsync();
        if (users < 1) {
          var userManager = applicationServices.GetService<UserManager<ApplicationUser>>();
          ApplicationUser newUser = new ApplicationUser();
          newUser.UserName = "admin";
          await userManager.CreateAsync(newUser, "Admin1!");
          await userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role,"Admin"));
          c.SaveChanges();
        }
        
      }
    }

    
  }
}
