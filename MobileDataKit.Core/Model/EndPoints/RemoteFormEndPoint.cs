using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model.EndPoints
{
  public  class RemoteFormEndPoint :Realms.RealmObject
    {
        [Realms.PrimaryKey]
        public string ID { get; set; }


        public string PrivateKeyID { get; set; }
        public string UserID { get; set; }


        public string DeviceID { get; set; }
        public string Role { get; set; }

        public string VirgilCard { get; set; }
    }
}
