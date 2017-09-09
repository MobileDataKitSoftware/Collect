using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using TinyPG;

namespace MobileDataKit_Collect.Droid.Expressions
{
 


    [Serializable]
    public class MdkParseTree : TinyPG.ParseTree , MobileDataKit.Core.Model.Flow.IParseTree
    {


        public MdkParseTree() : base()
        {

        }
      

       
        public override TinyPG.ParseNode CreateNode(Token token, string text)
        {
            TinyPG.ParseNode node = new MdkParseNode(token, text);
            node.Parent = this;
            return node;
        }

    }
}