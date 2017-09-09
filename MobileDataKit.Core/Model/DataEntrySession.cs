using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
  public  class DataEntrySession : Realms.RealmObject
    {

        [Realms.PrimaryKey]
        public string ID { get; set; }

        public EntryForm EntryForm { get; set; }


        public Form Form { get; set; }
    }
}
