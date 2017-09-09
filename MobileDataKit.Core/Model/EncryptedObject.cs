using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Model
{
  public  class EncryptedObject
    {

        public string Data { get; set; }

        public List<EncryptedObjectRecepient> Recepients { get; set; } = new List<EncryptedObjectRecepient>();
    }
}
