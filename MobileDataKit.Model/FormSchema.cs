using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model
{
  public  class FormSchema
    {

        public FormServer Form { get; set; }
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FormID { get; set; }

        public string FormSchemaData { get; set; }

        public string SchemaHash { get; set; }

        public DateTime VersionTimeStamp { get; set; } = DateTime.Now;

        public string schematype { get; set; }
    }
}
