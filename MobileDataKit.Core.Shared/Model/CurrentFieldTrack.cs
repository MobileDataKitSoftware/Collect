using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
    public class CurrentFieldTrack : Realms.RealmObject
    {


        [Newtonsoft.Json.JsonIgnore]
        public EntryForm EntryForm { get; set; }
        [Realms.PrimaryKey]
        public string ID { get; set; }

        public string SectionID { get; set; }
        public int No { get; set; }


        public string FieldName {get;set;}


        public override string ToString()
        {
            return No.ToString() + " - " + FieldName;
        }


    }
}
