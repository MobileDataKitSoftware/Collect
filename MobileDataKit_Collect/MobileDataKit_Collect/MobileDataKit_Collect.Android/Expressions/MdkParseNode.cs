using MobileDataKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using TinyPG;

namespace MobileDataKit_Collect.Droid.Expressions
{
  public  class MdkParseNode : TinyPG.ParseTree
    {


        public MdkParseNode(Token token, string text)
        {
            this.Token = token;
            this.text = text;
            this.nodes = new List<ParseNode>();
        }

        public override ParseNode CreateNode(Token token, string text)
        {
            ParseNode node = new MdkParseNode(token, text);
            node.Parent = this;
            return node;
        }
        private object GetValueFromTerminal(ParseNode n)
        {
            if (n.Token.Type == TokenType.NUMBER)
                return n.Token.Text;
            if (n.Token.Type == TokenType.String)
                return EvalString((MdkParseNode)n, null);
            if (n.Token.Type == TokenType.STAR)
                return "*";
            if (n.Token.Type == TokenType.TEXT)
                return n.Token.Text;
            return string.Empty;
        }

        protected override object EvalString(ParseTree tree, params object[] paramlist)
        {
            var str = string.Empty;
            foreach (var t in tree.Nodes)
            {
                if (t.Token.Type == TokenType.QUOTEBEGIN || t.Token.Type == TokenType.QUOTEEND)
                    str = str + "\'";
                if (t.Token.Type == TokenType.QUOTED)
                    str = str + t.Token.Text;


            }
            return str;
        }
        protected override object EvalStart(ParseTree tree, params object[] paramlist)
        {
            var dd = tree.Nodes[0];

            if (dd.Nodes[0].Token.Type == TokenType.CodeBlock)
                return EvalCodeBlock((MdkParseNode)dd.Nodes[0], paramlist);
            if (dd.Nodes[0].Token.Type == TokenType.CommandExpr )
                return EvalCommandExpr((MdkParseNode)dd.Nodes[0], paramlist);

            return base.EvalStart(tree, paramlist);
        }
        protected override object EvalCommandExpr(ParseTree tree, params object[] paramlist)
        {

            if (tree.Nodes[0].Token.Type == TokenType.RepeatExpr)
                return EvalRepeatExpr((MdkParseNode)tree.Nodes[0], paramlist);
            if (tree.Nodes[0].Token.Type == TokenType.ForeachExpr)
                return EvalForeachExpr((MdkParseNode)tree.Nodes[0], paramlist);
            if (tree.Nodes[0].Token.Type == TokenType.CondExpr)
                return EvalCondExpr((MdkParseNode)tree.Nodes[0], paramlist);
            //if (tree.Nodes[0].Token.Type == TokenType.ReplaceExpr)
            //    return EvalReplaceExpr((MyParseNode)tree.Nodes[0], paramlist);
            //if (tree.Nodes[0].Token.Type == TokenType.OpenFileExpr)
            //    return EvalOpenFileExpr((MyParseNode)tree.Nodes[0], paramlist);

            //if (tree.Nodes[0].Token.Type == TokenType.LogExpr)
            //    return EvalLogExpr((MyParseNode)tree.Nodes[0], paramlist);


            //if (tree.Nodes[0].Token.Type == TokenType.SortExpr)
            //    return EvalSortExpr((MyParseNode)tree.Nodes[0], paramlist);

            //if (tree.Nodes[0].Token.Type == TokenType.LabelExpr)
            //    return EvalLabelExpr((MyParseNode)tree.Nodes[0], paramlist);
            //if (tree.Nodes[0].Token.Type == TokenType.TitleExpr)
            //    return EvalTitleExpr((MyParseNode)tree.Nodes[0], paramlist);

            return base.EvalCommandExpr(tree, paramlist);
        }
        protected override object EvalRepeatExpr(ParseTree tree, params object[] paramlist)
        {

            if (tree.Nodes[1].Token.Type == TokenType.CondExpr)
            {
                var f= (bool)EvalCondExpr((MdkParseNode)tree.Nodes[1], paramlist);
                if (f == true)
                    return MobileDataKit.Core.Model.Flow.FlowResult.NextSubRecord;
                return MobileDataKit.Core.Model.Flow.FlowResult.NextField;
            }
              
            return base.EvalRepeatExpr(tree, paramlist);
        }


        protected object ExecuteWhile(ParseTree tree, params object[] paramlist)
        {

            var cond = bool.Parse (paramlist[0].ToString());
            
            return cond;
        }


        protected override object EvalForeachExpr(ParseTree tree, params object[] paramlist)
        {
            List<MobileDataKit.Core.Model.EntryForm> collection = null;

            if (tree.Nodes[3].Token.Type == TokenType.ValueExpres)
                collection = (List < MobileDataKit.Core.Model.EntryForm > )EvalValueExpres((MdkParseNode)tree.Nodes[3], paramlist);
            if (collection == null || collection.Count == 0)
                return null;
            if (paramlist.Count() == 2)
                return collection[0];


            if(paramlist.Count() ==3)
            {
                MobileDataKit.Core.Model.EntryForm current = (MobileDataKit.Core.Model.EntryForm )paramlist[2];
                try
                {
                    return collection[collection.IndexOf(current) + 1];
                }
                catch
                {
                    return null;
                }
                
            }

            return base.EvalForeachExpr(tree, paramlist);
        }
        protected override object EvalCondExpr(ParseTree tree, params object[] paramlist)
        {

            List<ParseNode> nodes = new List<ParseNode>();
            List<ParseNode> joins = new List<ParseNode>();
            List<object> condition_results = new List<object>();
            object f = null;

            f = EvalConditionClause((MdkParseNode)tree.Nodes[1], paramlist).ToString();



            var clause = tree.Nodes[0];
            var n = tree.Nodes[1];


           

            //if (n.Token.Type == TokenType.JoinCondition)
            //    f = f + EvalJoinCondition((MdkParseNode)n, paramlist).ToString();
            //if (n.Token.Type == TokenType.ConditionBracketed)
            //    f = f + EvalConditionBracketed((MdkParseNode)n, paramlist).ToString();

            if (clause.Token.Type == TokenType.WHILE)
               return ExecuteWhile((MdkParseNode)n, f);

            if (f != null)
                return f;
                throw new NotImplementedException();


            // return base.EvalCondExpr(tree, paramlist);
        }


        protected override object EvalJoinCondition(ParseTree tree, params object[] paramlist)
        {
            return tree.Nodes[0].Token.Type;
        }

        protected override object EvalConditionClause(ParseTree tree, params object[] paramlist)
        {
            object f = null;
            var results = new Dictionary<int, object>();
            var c = -1;

            TokenType condition =TokenType._UNDETERMINED_;
            if(tree.Nodes.Count >1 && tree.Nodes[1].Token.Type ==TokenType.JoinCondition)
                condition= (TokenType)EvalJoinCondition((MdkParseNode)tree.Nodes[1], paramlist);
            var first_result = EvalConditionComparer((MdkParseNode)tree.Nodes[0], paramlist).ToString();
            if (condition == TokenType._UNDETERMINED_)
                return first_result;

                if (condition == TokenType.OR)
            {
                if (first_result != null && bool.Parse(first_result))
                    return true;
                return EvalConditionComparer((MdkParseNode)tree.Nodes[2], paramlist).ToString();

            }
           

               

                

            
           

            return base.EvalConditionClause(tree, paramlist);
        }
        private object GetValue(ParseNode node, params object[] paramlist)
        {          

          var result=  GetValueFromTerminal(node);
            if (result != null && !string.IsNullOrWhiteSpace(result.ToString()))
                return result;
            if (node.Token.Type == TokenType.ValueExpres)
             return  EvalValueExpres((MdkParseNode)node, paramlist).ToString();
            return null;
        }
        protected override object EvalConditionComparer(ParseTree tree, params object[] paramlist)
        {
            LogicalEqualitySymbol condition= LogicalEqualitySymbol.None;
            var fff = tree.Nodes[0];
            var val1 = GetValue(fff, paramlist);

            var xfff = tree.Nodes[1];
            if (xfff.Token.Type == TokenType.EqualitySymbol)
                condition = (LogicalEqualitySymbol) EvalEqualitySymbol((MdkParseNode)xfff, paramlist);

            var xfffs = tree.Nodes[2];


            var val2 = GetValue(xfffs, paramlist);

            if (val1 ==null)
                val1 = string.Empty;

            if (val2 == null)
                val2 = string.Empty;
            if (condition ==LogicalEqualitySymbol.LessThan || condition == LogicalEqualitySymbol.GreaterThan)
            {
                double left = 0;
                double.TryParse(val1.ToString(), out left);
                double right = 0;
                double.TryParse(val2.ToString(), out right);
                if (condition == LogicalEqualitySymbol.LessThan)
                    return left < right;
                if (condition == LogicalEqualitySymbol.GreaterThan)
                    return left > right;

            }


            if (condition == LogicalEqualitySymbol.Equal  )
            {
                
                return val1.ToString() == val2.ToString();

            }


            if (condition == LogicalEqualitySymbol.NotEqual)
            {
                return val1.ToString() != val2.ToString();

            }
            return false;



        }


       

        protected override object EvalValueExpres(ParseTree tree, params object[] paramlist)
        {
            MobileDataKit.Core.Model.EntryForm form = (MobileDataKit.Core.Model.EntryForm)paramlist[0];
            if (tree.Nodes.Count ==1)
            {
                var obj = tree.Nodes[0];
                if (obj.Token.Type == TokenType.OBJECT)
                {
                    Object res=null;
                    foreach (var i in form.EntryVariables)
                    {
                        if (i.FieldID == obj.Token.Text)
                        {
                            if (i.Value == null)
                                return int.MaxValue;
                            res = i.Value;


                            break;

                        }



                    }
                    try
                    {
                        return (from i in form.EntryVariables where i.FieldID == obj.Token.Text select i).First().OldValue;
                    }
                    catch
                    {

                    }



                }
            }
            object obj_evaluated = null;
            foreach (var obj in tree.Nodes)
            {
             
                if (obj.Token.Type == TokenType.OBJECT)
                {

                    try
                    {
                        obj_evaluated = (from i in form.EntryVariables where i.ID == obj.Token.Text select i).First().Value;
                    }
                    catch
                    {



                        try
                        {
                            var variablename = obj.Token.Text;

                            obj_evaluated = App.realm.All<MobileDataKit.Core.Model.EntryVariable>().Where(f => f.FieldID == variablename && f.EntryFormID == form.ID).First();

                        }
                        catch
                        {
                            obj_evaluated = ResultType.NULL;
                        }
                           
                    }



                }

                if (obj.Token.Type == TokenType.ArrayExpr)
                {
                    obj_evaluated = EvalArrayExpr((MdkParseNode)obj,obj_evaluated);
                }


                if (obj.Token.Type == TokenType.COUNT || obj.Token.Type == TokenType.LENGTH)
                {
                    obj_evaluated = EvalArrayCommand((MdkParseNode)obj, obj_evaluated);
                }


            }

            if (obj_evaluated != null)
                return obj_evaluated;
            throw new NotImplementedException();
            return base.EvalValueExpres(tree, paramlist);
        }

        protected override object EvalConditionBracketed(ParseTree tree, params object[] paramlist)
        {
            var fff = "(" + EvalCondExpr(tree, paramlist).ToString() + ")";

            return fff;
        }
        protected  object EvalArrayCommand(ParseTree tree, params object[] paramlist)
        {

            List<MobileDataKit.Core.Model.EntryForm> list = (List < MobileDataKit.Core.Model.EntryForm> )paramlist[0];

            if (tree.Token.Type == TokenType.COUNT)
                return list.Count;

            return null;
        }
        protected override object EvalEqualitySymbol(ParseTree tree, params object[] paramlist)
        {
            var ddd = tree;
            if (tree.Nodes[0].Token.Type == TokenType.EQUALITY)
                return LogicalEqualitySymbol.Equal;

            if (tree.Nodes[0].Token.Type == TokenType.NOTEQUAL)
                return LogicalEqualitySymbol.NotEqual;
            if (tree.Nodes[0].Token.Type == TokenType.LESSTHAN)
                return LogicalEqualitySymbol.LessThan;

            if (tree.Nodes[0].Token.Type == TokenType.GREATERTHAN)
                return LogicalEqualitySymbol.GreaterThan;
            return string.Empty;
        }
        protected override object EvalArrayExpr(ParseTree tree, params object[] paramlist)
        {
            //array variable 
            MobileDataKit.Core.Model.EntryVariable vari = null;

          vari =  paramlist[0] as MobileDataKit.Core.Model.EntryVariable;
            if(vari ==null && paramlist.Count()>1)
                vari = paramlist[1] as MobileDataKit.Core.Model.EntryVariable;

            var array_key = vari.ID;
            var array_values = new List<MobileDataKit.Core.Model.EntryForm>();

            array_values = App.realm.All<MobileDataKit.Core.Model.EntryForm>().Where(c => c.FieldID == array_key).OrderBy(c=>c.FormIndexNo).ToList();

            //we check if the array is empty []
            if (tree.Nodes.Count ==2)
            {
                return array_values;
            }
            throw new NotImplementedException();

            return base.EvalArrayExpr(tree, paramlist);
        }
    }
}




public enum LogicalEqualitySymbol
{
    None,
    LessThan,
    GreaterThan,
    Equal,
    NotEqual


}

public enum ResultType
{
    NULL
}