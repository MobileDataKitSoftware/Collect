using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace MobileDataKit.Model.Account
{
  public  class UserDbContext: IdentityDbContext<ApplicationUser>
    {


        public UserDbContext(DbContextOptions options): base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(Settings.Configuration.Connection["DefaultConnection"]);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
