using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model
{
   public class ProjectUser
    {
        public ICollection<FormUser> FormUsers { get; set; }
        public Guid ID { get; set; }

        public Guid? ProjectID { get; set; }

        public Guid? CompanyUserID { get; set; }


        public CompanyDevice CompanyDevice { get; set; }

        public Guid? CompanyDeviceID { get; set; }
    }
}
