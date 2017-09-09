using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Model
{
  public partial  class AppConstants
    {
        public static string Url
        {
            get
            {
               return "http://" + Host + ":" + Port.ToString() + "/";
            }
        }

        public static string Host = "192.168.43.64";

        public static int Port = 11921;
    }
}
