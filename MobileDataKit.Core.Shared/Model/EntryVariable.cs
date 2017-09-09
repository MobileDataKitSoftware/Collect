using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
public    class EntryVariable:Realms.RealmObject
    {

        public string FieldID { get; set; }

        public string EntryFormID { get; set; }
#if SERVER
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
         private string Value_ ;
#else
        private string Value_ { get; set; }
#endif
        public string RowVersion { get; set; }
        public string Value
        {
            get
            {
                return Value_;
            }
            set
            {
                OldValue = Value_;
                Value_ = value;
                IsDirty = true;
            }
        }

       
        public string OldValue { get; set; }

        public bool IsDirty { get; set; }

        [Realms.PrimaryKey]
        public string ID { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public EntryForm EntryForm { get; set; }
    }
}
