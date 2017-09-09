using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Encryption
{
 public   interface IEncryptionFactory
    {

        string Encrypt(object Data, List<MobileDataKit.Core.Model.EndPoints.RemoteFormEndPoint> keys);
    }
}
