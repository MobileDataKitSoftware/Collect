using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
public    interface ISection
    {
         IList<Core.Model.Field> Fields { get; }

        Core.Model.Field GetNextField(string sectionid,Field r=null);
         string CurrentField { get; set; }
        string Name { get; set; }

        string Text { get; set; }
        string Expression { get; set; }



    }
}
