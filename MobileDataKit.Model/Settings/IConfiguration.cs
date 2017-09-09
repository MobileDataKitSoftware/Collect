using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model.Settings
{
   public interface IConfiguration
    {
        Dictionary<string , string> Connection { get; set; }
    }
}
