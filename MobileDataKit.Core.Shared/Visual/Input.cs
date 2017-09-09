using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileDataKit.Core.Model;

namespace MobileDataKit.Core.Visual
{
    public class Input:BaseElement
    {


        public override Field GetModel()
        {
            var f = new Model.Field();
            f.ControlType = type;
            f.Text = Label;
            f.Required = true;
            return f;
        }
    }
}
