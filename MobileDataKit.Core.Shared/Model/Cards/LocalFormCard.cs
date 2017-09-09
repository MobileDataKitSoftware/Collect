using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
 public   class LocalFormCard : Realms.RealmObject
    {
        public LocalFormCard()
        {

        }

        [Realms.PrimaryKey]
        public string ID { get; set; }

        public string Card { get; set; }
    }
}
