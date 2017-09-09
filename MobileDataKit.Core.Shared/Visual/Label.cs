using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Visual
{
  public  class Label:BaseElement
    {

        public string Text { get; set; }




        public override Model.Field GetModel()
        {
            var f = new Model.Field();


            return f;
        }
    }
}
