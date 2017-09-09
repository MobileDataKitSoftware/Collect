using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Model
{
  public  class FormUser
    {

        public Guid ID { get; set; } = Guid.NewGuid();

        public Guid FormID { get; set; }

        public string Role { get; set; } = "Collector";

        public Guid ProjectUserID { get; set; }
    }
}
