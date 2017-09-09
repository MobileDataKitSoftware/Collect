using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
  public  class Project : Realms.RealmObject
    {

        [Realms.PrimaryKey]
        public string ID { get; set; }


        public override string ToString()
        {
            return this.ProjectName;  
        }
        public string ProjectName { get; set; }




       

        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string PublicKey { get; set; }


      //  public ICollection<ProjectUser> ProjectUsers { get; set; }
        public IList<Form> Forms { get;  }


#if SERVER
        public Project()
        {

        this.Forms =new List<Form>();
        }
#endif

    }
}
