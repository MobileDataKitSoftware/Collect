using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
public    interface ISection
    {
         IList<Core.Model.Field> Fields { get; }

        Core.Model.Field GetNextField();
         string CurrentField { get; set; }
        string Name { get; set; }

      

    }
}
