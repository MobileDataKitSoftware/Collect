using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model
{
public    class FormServer : MobileDataKit.Core.Model.Form
    {
       
      

        public string PublicKey { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public MobileDataKit.Core.Model.Form VisualForm { get; set; }


        public ICollection<FormUser> FormUsers { get; set; }
        public ICollection<FormSchema> FormSchemas { get; set; }
        
    }
}
