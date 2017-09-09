using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Auth
{
  public  interface ICredentialManager
    {

        void SaveCredentials(string userName, string password,string token);
    }
}
