using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Model
{
  public  class Configuration
    {

        public Configuration()
        {

        }
        public ConnectionStrings ConnectionStrings { get; set; }
    }


    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}
