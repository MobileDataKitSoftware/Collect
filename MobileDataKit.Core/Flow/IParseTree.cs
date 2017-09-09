using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Model.Flow
{
 public   interface IParseTree
    {
        object Eval(params object[] paramlist);
    }
}
