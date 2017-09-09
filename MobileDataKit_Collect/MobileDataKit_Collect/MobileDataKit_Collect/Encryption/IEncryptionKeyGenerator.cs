using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Encryption
{
  public  interface IEncryptionKeyGenerator
    {
        byte[] GetKey(string Password, string username);
        string Generate(string name);
    }
}
