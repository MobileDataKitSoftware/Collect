using MobileDataKit.Core;
using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if SERVER
#else
using Xamarin.Forms;
using MobileDataKit_Collect;
#endif

namespace MobileDataKit_Collect.Model
{
    public class FlowManager
    {
        public EntryVariable CreateEntryVariable(object value=null)
        {
            var d1 = isection.Name;
            EntryVariable entryVariable = null;
            if (EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == d1).Count() > 0)
                entryVariable = EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == d1).First();
            else
            {
                entryVariable = new EntryVariable();
                entryVariable.EntryForm = EntryForm.CurrentEntryForm;
                entryVariable.EntryFormID = entryVariable.EntryForm.ID;
                entryVariable.FieldID = d1;
                entryVariable.ID = Guid.NewGuid().ToString();
                if(value !=null)
                entryVariable.Value = value.ToString();
                EntryForm.CurrentEntryForm.EntryVariables.Add(entryVariable);
            }

            entryVariable.OldValue = entryVariable.Value;
            return entryVariable;
        }
        private object ExpressionResult = null;

       internal ISection isection = null;
        private void CreateRecord(object expr_result, EntryVariable next_entry)
        {
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();

            if (typeof(MobileDataKit.Core.Model.Flow.FlowResult) == expr_result.GetType())
            {
                MobileDataKit.Core.Model.Flow.FlowResult result = (MobileDataKit.Core.Model.Flow.FlowResult)expr_result;
                if (result == MobileDataKit.Core.Model.Flow.FlowResult.NextSubRecord)
                {
                    var entry = MobileDataKit.Core.Model.EntryForm.CreateNewEntry(isection);
                    MobileDataKit.Core.Model.EntryForm.CurrentEntryForm = entry;
                    entry.FormIndexNo = App.realm.All<MobileDataKit.Core.Model.EntryForm>().Where(ef => ef.FieldID == next_entry.ID).Count() + 1;
                    entry.FieldID = next_entry.ID;
                    EntryTransaction.DefaultTransaction().CommitAndStartTransaction();



                }
                if (result == MobileDataKit.Core.Model.Flow.FlowResult.NextField)
                {




                    throw new EndOfRepeatException();


                }


            }
        }
        public MobileDataKit.Core.Model.Field GetNextField(ISection item, string sectionid, Field current = null)
        {

            isection = item;

            var parser = DependencyService.Get<MobileDataKit.Core.Model.Flow.IParser>();
            EntryVariable base_entry = null;
            EntryForm base_EntryForm = null;
            var _id = item.Name;
            var _name = item.Name;
            Guid d2;
            bool create_new_record = false;
            string FieldID = string.Empty;
            var fi = item as Field;
            if (fi != null)
                FieldID = fi.FieldID;
            if (string.IsNullOrWhiteSpace(FieldID) || Guid.TryParse(FieldID, out d2))
            {
                _id = item.Text;
                _name = item.Text;
            }
            if (ExpressionResult == null)
            {
                if (!string.IsNullOrWhiteSpace(item.Expression))
                {

                    foreach (var a in EntryForm.EntrySessionForms)
                    {
                        if ((from v in a.EntryVariables where v.FieldID == _id select v).Count() > 0)
                        {
                            base_entry = (from v in a.EntryVariables where v.FieldID == _id select v).First();
                            _id = base_entry.FieldID;
                            base_EntryForm = a;
                            break;
                        }

                    }

                    if (base_EntryForm == null)
                        base_EntryForm = EntryForm.CurrentEntryForm;
                    try
                    {

                        ExpressionResult = parser.ParseExpression(item.Expression).Eval(base_EntryForm, this);
                    }
                    catch
                    {

                    }
                    if (_name == _id && base_entry == null && ExpressionResult == null)
                    {
                        create_new_record = true;
                        base_entry = CreateEntryVariable();
                        base_entry.FieldID = item.Name;
                        base_entry.EntryForm = base_EntryForm;
                        base_entry.EntryFormID = base_EntryForm.ID;
                        base_EntryForm.EntryVariables.Add(base_entry);

                        if (string.IsNullOrWhiteSpace(base_entry.FieldID) || Guid.TryParse(base_entry.FieldID, out d2))
                            base_entry.FieldID = item.Text;

                    }

                    try
                    {

                        if (ExpressionResult == null)
                            ExpressionResult = parser.ParseExpression(item.Expression).Eval(base_EntryForm, this);

                    }
                    catch
                    {

                    }
                }

                if (ExpressionResult != null && typeof(MobileDataKit.Core.Model.Flow.FlowResult) == ExpressionResult.GetType() && create_new_record)
                {
                   isection.CurrentField = string.Empty;
                    CreateRecord(ExpressionResult, base_entry);





                }

                if (ExpressionResult != null && typeof(EntryForm) == ExpressionResult.GetType())
                {
                    EntryForm.CurrentEntryForm = (EntryForm)ExpressionResult;

                }



            }

            Field return_field = null;
            Field _current_field = null;
            if (item.Fields.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(EntryForm.CurrentEntryForm.GetCurrentField(sectionid)) && current == null)
                {
                   isection.CurrentField = item.Fields[0].Name;
                    return_field = item.Fields[0];
                }
                if (current != null)
                    _current_field = (from v in item.Fields where v.Name == current.Name select v).First();
                else
                if (!string.IsNullOrWhiteSpace(EntryForm.CurrentEntryForm.GetCurrentField(sectionid)))
                    _current_field = (from v in item.Fields where v.Name == EntryForm.CurrentEntryForm.GetCurrentField(sectionid) select v).First();
                if (return_field == null)
                    try
                    {
                        var next_field = item.Fields[item.Fields.IndexOf(_current_field) + 1];
                        isection.CurrentField = next_field.Name;
                        return_field = next_field;


                    }
                    catch
                    {

                    }


            }

            if (return_field != null)
            {
                if (string.IsNullOrWhiteSpace(return_field.Precondition))
                    return return_field;

                // we have precondition expression
                try
                {


                    var res = parser.ParseExpression(return_field.Precondition).Eval(EntryForm.CurrentEntryForm, return_field);
                    if ((bool.Parse(res.ToString()) != true))
                    {
                        
                        return_field.CreateEntryVariable().Value = null;
                        EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
                        return_field = GetNextField(item, sectionid, return_field);
                    }
                        

                }
                catch(Exception ex)
                {
                    var rrr = ex;


                }


                return return_field;

            }

           isection.CurrentField = string.Empty;

            if (ExpressionResult != null && typeof(MobileDataKit.Core.Model.Flow.FlowResult) == ExpressionResult.GetType() && ((MobileDataKit.Core.Model.Flow.FlowResult)ExpressionResult) == MobileDataKit.Core.Model.Flow.FlowResult.NextSubRecord)
            {
                if (ExpressionResult != null)
                    EntryForm.RemoveCurrentForm();
                ExpressionResult = null;
                CreateRecord(ExpressionResult, base_entry);
                return GetNextField(isection,sectionid);
            }

            if (ExpressionResult != null && typeof(EntryForm) == ExpressionResult.GetType())
            {

                if (base_EntryForm == null)
                    base_EntryForm = EntryForm.CurrentEntryForm.GetAdjacentAboveForm();

                try
                {


                    ExpressionResult = parser.ParseExpression(isection.Expression).Eval(base_EntryForm, this, EntryForm.CurrentEntryForm);
                }
                catch
                {

                }
                EntryForm.RemoveCurrentForm();




                if (ExpressionResult == null)
                    return null;
                EntryForm.CurrentEntryForm = (EntryForm)ExpressionResult;

                return GetNextField(item,sectionid);
            }
            return null;

        }

    }
}
