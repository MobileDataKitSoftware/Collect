using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model
{
public    class Company
    {

       
        
        [System.ComponentModel.DataAnnotations.Key]
        public string Email { get; set; }
        public string CompanyName { get; set; }

        public string Country { get; set; }

        public string Industry { get; set; }


        public string PublicKey { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string AdminEmail { get; set; }


        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string AdminPassword { get; set; }

        public List<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();



        public List<CompanyDevice> CompanyDevices { get; set; }
    }
}
