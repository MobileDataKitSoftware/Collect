using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockApp.EntryControls
{
  public  class SectionLabel : BaseControl
    {

        public SectionLabel()
        {
          
        }
        public SectionLabel(ISection section) : base(null, section)
        {


            var l = new System.Windows.Forms.Label();
            l.Text = section.Name;
            this.Controls.Add(l);

        }

    }
}
