using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Model
{


    public partial class FieldOption : Realms.RealmObject
    {
        [Newtonsoft.Json.JsonIgnore]
        public Field Field { get; set; }
        [Realms.PrimaryKey]
        public string ID { get; set; }
        public string FieldID { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }


        public override string ToString()
        {
            return No.ToString() + " - " + Name + " - " + Value;
        }

        public double? No { get; set; }

        public string Expression { get; set; }

#if SERVER

        public FieldOption()
        {
            this.ID = Guid.NewGuid().ToString();
        }
#endif
    }
}
