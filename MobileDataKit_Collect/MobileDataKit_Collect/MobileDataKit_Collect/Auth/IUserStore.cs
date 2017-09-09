using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Auth
{
  public  interface IUserStore
    {

        void RefreshValue(string username, string password);

        string UserReam { get; set; }
         string PasswordHash { get; set; }
    }
}
