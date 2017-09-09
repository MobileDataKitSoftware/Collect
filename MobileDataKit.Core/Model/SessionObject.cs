using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
    public class SessionObject : Realms.RealmObject
    {

        public string SessionID { get; set; }


        public string ObjectID { get; set; }


        public string ObjectName {get;set;}
    }
}
