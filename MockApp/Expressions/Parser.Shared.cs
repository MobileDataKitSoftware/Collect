using MobileDataKit.Core.Model.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyPG
{
    public partial class Parser : IParser
    {

        public Parser()
        {
            this.scanner = new TinyPG.Scanner();
        }
        public MobileDataKit.Core.Model.Flow.IParseTree ParseExpression(string expression)
        {
            return (IParseTree)this.Parse(expression, new MobileDataKit_Collect.Droid.Expressions.MdkParseTree());
        }
    }
}