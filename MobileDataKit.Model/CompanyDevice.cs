using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Model
{
  public  class CompanyDevice
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public string DeviceName { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

        public Device Device { get; set; }

        public string IMEI { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string DeviceDisplayName
        {
            get
            {
                return DeviceName + " - " + IMEI;
            }
        }

        public string CompanyEmail { get; set; }

        public Company Company { get; set; }
     public   List<ProjectUser> ProjectUsers { get; set; }
    }
}
