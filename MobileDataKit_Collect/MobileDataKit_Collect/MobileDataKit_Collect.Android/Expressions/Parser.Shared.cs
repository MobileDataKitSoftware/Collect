using MobileDataKit.Core.Model.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[assembly: Xamarin.Forms.Dependency(typeof(TinyPG.Parser))]
namespace TinyPG
{
    public partial class Parser : IParser
    {

        public Parser()
        {
            this.scanner = new TinyPG.Scanner();
        }
        public IParseTree ParseExpression(string expression)
        {
            return (IParseTree) this.Parse(expression, new MobileDataKit_Collect.Droid.Expressions.MdkParseTree());
        }
    }
}