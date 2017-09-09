using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Visual
{
    public class BaseElement
    {

        public virtual Core.Model.Field GetModel()
        {
            return null;
        }
        public string Ref { get; set; }

        public string Label { get; set; }


        public string required { get; set; }

        public string type { get; set; }

        public string nodeset { get; set; }
        public string relevant { get; set; }


        public string VariableName
        {
            get
            {
                return Ref;
            }
        }
    }
}
