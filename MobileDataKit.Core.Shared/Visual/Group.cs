using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Visual
{
    public class Group : BaseElement, ICollector
    {
        public List<BaseElement> Elements { get; set; } = new List<BaseElement>();
    }
}
