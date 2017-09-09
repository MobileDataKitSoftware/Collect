using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model.Account
{
   public class ApplicationUser : IdentityUser
    {

        public Guid? CurrentCompany { get; set; }

        public string Name { get; set; }

       
    }
}
