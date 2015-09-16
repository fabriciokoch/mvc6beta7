﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MyApp.Models;
using Microsoft.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApp.Api {
  [Route("api/[controller]")]
  public class UsersController : Controller {


    private readonly MyAppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(MyAppDbContext dbContext, UserManager<ApplicationUser> userManager) {
      _dbContext = dbContext;
      _userManager = userManager;
    }

    // GET: api/values
    [HttpGet]
    public IEnumerable<ApplicationUser> Get() {
      return _dbContext.Users;
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ApplicationUser Get(string id) {
      return _dbContext.Users.Include(user => user.Claims).Where(user => user.Id == id).FirstOrDefault();
    }

    // POST api/values
    [HttpPost("{userId}/{claimName}")]
    public async Task<ApplicationUser> Post(string userId, string claimName) {
      var user = _dbContext.Users.Include(u => u.Claims).Where(u => u.Id == userId).FirstOrDefault();
      await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claimName));
      _dbContext.SaveChanges();
      return user;
    }

    // DELETE api/values/5
    [HttpDelete("{userId}/{claimName}")]
    public async Task Delete(string userId, string claimName) {
      var user = _dbContext.Users.Include(u => u.Claims).Where(u => u.Id == userId).FirstOrDefault();
      await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, claimName));
      _dbContext.SaveChanges();
    }
  }
}
