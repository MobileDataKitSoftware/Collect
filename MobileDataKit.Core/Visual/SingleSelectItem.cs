using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Visual
{
 public   class SingleSelectItem :BaseElement
    {
        public override Field GetModel()
        {
            var f = new Model.Field();


            return f;
        }

        private object _value = null;
        public object value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
               
             

            }
        }
    }
}
