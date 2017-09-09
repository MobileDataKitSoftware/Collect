using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Model
{
public    class Device
    {

        [System.ComponentModel.DataAnnotations.Key]
        public string IMEI { get; set; }


        public string CreatedBy { get; set; }
        public ICollection<CompanyDevice> CompanyDevices { get; set; }
    }
}
