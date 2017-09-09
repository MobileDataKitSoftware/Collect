using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Auth
{
  public  interface ILogin
    {

        void Login(string username, string password);
    }
}
