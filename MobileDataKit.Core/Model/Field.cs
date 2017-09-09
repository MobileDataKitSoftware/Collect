
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MobileDataKit.Core.Model
{

   
    
    public partial class Field : Realms.RealmObject, ISection
    {
        public void UpdateValues(Field f)
        {
            this.Text = f.Text;
            this.ControlType = f.ControlType;
            this.Name = f.Name;
            this.No = f.No;
            

        }

        public string CurrentField { get; set; }


        private void CreateRecord(object expr_result, EntryVariable next_entry)
        {
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            
            if (typeof(Flow.FlowResult) == expr_result.GetType())
            {
                Flow.FlowResult result = (Flow.FlowResult)expr_result;
                if (result == Flow.FlowResult.NextSubRecord)
                {
                    var entry = MobileDataKit.Core.Model.EntryForm.CreateNewEntry(this);
                    MobileDataKit.Core.Model.EntryForm.CurrentEntryForm = entry;
                    entry.FormIndexNo = App.realm.All<MobileDataKit.Core.Model.EntryForm>().Where(ef => ef.FieldID == next_entry.ID).Count() + 1;
                    entry.FieldID = next_entry.ID;
                    EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
                    


                }
                if (result == Flow.FlowResult.NextField)
                {




                    throw new EndOfRepeatException();


                }


            }
        }

       
        private object ExpressionResult=null;
        public  Core.Model.Field GetNextField()
        {
            var parser = DependencyService.Get<Flow.IParser>();
            EntryVariable base_entry = null;
            EntryForm base_EntryForm = null;
            var _id = Name;
            var _name = Name;
            Guid d2;
            bool create_new_record = false;
            if (string.IsNullOrWhiteSpace(FieldID) || Guid.TryParse(FieldID, out d2))
            {
                _id = Text;
                _name = Text;
            }
            if (ExpressionResult == null)
            {
                if (!string.IsNullOrWhiteSpace(Expression))
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

                        ExpressionResult = parser.ParseExpression(Expression).Eval(base_EntryForm, this);
                    }
                    catch
                    {

                    }
                    if (_name == _id && base_entry == null && ExpressionResult == null)
                    {
                        create_new_record = true;
                        base_entry = CreateEntryVariable();
                        base_entry.FieldID = Name;
                        base_entry.EntryForm = base_EntryForm;
                        base_entry.EntryFormID = base_EntryForm.ID;
                        base_EntryForm.EntryVariables.Add(base_entry);

                        if (string.IsNullOrWhiteSpace(base_entry.FieldID) || Guid.TryParse(base_entry.FieldID, out d2))
                            base_entry.FieldID = Text;

                    }

                    if (ExpressionResult == null)
                        ExpressionResult = parser.ParseExpression(Expression).Eval(base_EntryForm, this);
                }

                if (ExpressionResult != null && typeof(Flow.FlowResult) == ExpressionResult.GetType() && create_new_record)
                {
                    CurrentField = string.Empty;
                    CreateRecord(ExpressionResult, base_entry);





                }

                if (ExpressionResult != null && typeof(EntryForm) == ExpressionResult.GetType())
                {
                    EntryForm.CurrentEntryForm = (EntryForm)ExpressionResult;
                    EntryForm.CurrentEntryForm.CurrentField = string.Empty;
                }



            }
            if ( Fields.Count >0)
            {
                if(string.IsNullOrWhiteSpace(EntryForm.CurrentEntryForm.CurrentField))
                {
                    CurrentField = Fields[0].Name;
                    EntryForm.CurrentEntryForm.CurrentField = CurrentField;
                    return Fields[0];
                }

                var _current_field = (from v in Fields where v.Name == EntryForm.CurrentEntryForm.CurrentField select v).First();
                try
                {
                    var next_field = Fields[Fields.IndexOf(_current_field) + 1];
                    CurrentField = next_field.Name;
                    EntryForm.CurrentEntryForm.CurrentField = CurrentField;
                    return next_field;
                }
                catch
                {
                    
                }


            }

            CurrentField = string.Empty;

            if(ExpressionResult != null  && typeof(Flow.FlowResult) == ExpressionResult.GetType() && ((Flow.FlowResult)ExpressionResult) ==Flow.FlowResult.NextSubRecord)
            {
                if (ExpressionResult != null)
                    EntryForm.RemoveCurrentForm();
                ExpressionResult = null;
                CreateRecord(ExpressionResult, base_entry);
                return GetNextField();
            }

            if(ExpressionResult !=null && typeof(EntryForm) == ExpressionResult.GetType())
            {

                if (base_EntryForm == null)
                    base_EntryForm = EntryForm.CurrentEntryForm.GetAdjacentAboveForm();

                ExpressionResult = parser.ParseExpression(Expression).Eval(base_EntryForm, this,EntryForm.CurrentEntryForm);
                EntryForm.RemoveCurrentForm();




                if (ExpressionResult == null)
                    return null;
                EntryForm.CurrentEntryForm =(EntryForm) ExpressionResult;
                EntryForm.CurrentEntryForm.CurrentField = string.Empty;
                return GetNextField();
            }
            return null;

        }

        public EntryVariable CreateEntryVariable()
        {
            var d1 = Name;
            EntryVariable entryVariable = null;
            if (EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == d1).Count() > 0)
                entryVariable = EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == d1).First();
            else
            {
                entryVariable = new EntryVariable();
                entryVariable.EntryForm = EntryForm.CurrentEntryForm;
                entryVariable.EntryFormID = entryVariable.EntryForm.ID;
                entryVariable.FieldID = Name;
                entryVariable.ID = Guid.NewGuid().ToString();
                
                 EntryForm.CurrentEntryForm.EntryVariables.Add(entryVariable);
            }

            entryVariable.OldValue = entryVariable.Value;
            return entryVariable;
        }

        public void SetChilds()
        {
            foreach(var child in Fields)
            {
                child.ParentField = this;
                this.Form = null;
                child.FieldID = this.Name;
                child.SetChilds();
            }

            foreach (var opt in this.FieldOptions)
                opt.Field = this;
        }
        public string FormID { get; set; }
        public string SectionID { get; set; }
       

       public string Precondition { get; set; }


        public string PostCondition { get; set; }
      

        public string Expression { get; set; }
        public  bool ShowInDashBoard { get; set; }


        public string FieldID { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return Name + "- " + Text;
        }

        public string Text { get; set; }

        public string ControlType { get; set; }
        [Realms.PrimaryKey]
#if SERVER
        [System.ComponentModel.DataAnnotations.Key]
#endif
        public string Name { get; set; }
        public Boolean Required { get; set; }

        public int No { get; set; }

        public IList<Field> Fields { get; }

        
        [Newtonsoft.Json.JsonIgnore]
        public Field ParentField { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public Form Form { get; set; }
        public IList<FieldOption> FieldOptions { get; }

#if SERVER

        public Field()
        {
            this.FieldOptions = new List<FieldOption>();
            this.Fields = new List<Field>();
         this.Name =Guid.NewGuid().ToString();
        }

#endif
    }
}
