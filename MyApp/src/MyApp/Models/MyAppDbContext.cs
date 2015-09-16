using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyApp.Models {

  public class ApplicationUser : IdentityUser { }

  public class MyAppDbContext : IdentityDbContext<ApplicationUser> {

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyApp;Trusted_Connection=True;MultipleActiveResultSets=true");
    }
  }
}
