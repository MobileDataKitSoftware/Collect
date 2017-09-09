using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model
{
  public  class CompanyUser
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string CompanyEmail { get; set; }

        public string UserID { get; set; }

        public string Role { get; set; } = "Collector";

        public Company Company { get; set; }
    }
}
