using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Model
{
  public  class EncryptedObjectRecepient
    {

        public string UserID { get; set; }
        public string DeviceID { get; set; }

        public string Key { get; set; }


        public string RecepientKey { get; set; }
    }
}
