using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using MobileDataKit.Core.Model;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.EntryControls
{
 public   class BaseControl: ContentPage
    {
        public virtual void SetNull()
        {
         try
            {
                entryVariable.Value = null;
            }
            catch
            {

            }

    }

        public event NextStepEvent nextstepevent;
        public delegate void NextStepEvent(object obj, NextStepEventArgs e);

        public static Dictionary<Field, ISection> ControlFlowState = new Dictionary<Field, ISection>();
        protected void OnNextStepEvent(NextStepEventArgs e)
        {
            if(nextstepevent !=null)
            nextstepevent(this, e);
        }

        protected async Task CreateRecord(MobileDataKit.Core.Model.Field next_field, EntryVariable next_entry)
        {
           
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            var parser = DependencyService.Get<MobileDataKit.Core.Model.Flow.IParser>();
            var f = parser.ParseExpression(next_field.Expression).Eval(next_field);
            if (typeof(MobileDataKit.Core.Model.Flow.FlowResult) == f.GetType())
            {
                MobileDataKit.Core.Model.Flow.FlowResult result = (MobileDataKit.Core.Model.Flow.FlowResult)f;
                if (result == MobileDataKit.Core.Model.Flow.FlowResult.NextSubRecord)
                {   var entry = MobileDataKit.Core.Model.EntryForm.CreateNewEntry(next_field);
                    MobileDataKit.Core.Model.EntryForm.CurrentEntryForm = entry;
                    entry.FormIndexNo = App.realm.All<MobileDataKit.Core.Model.EntryForm>().Where(ef => ef.FieldID == next_entry.ID).Count() + 1;
                    entry.FieldID = next_entry.ID;
                    EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
                    await this.Navigation.PushAsync((new EntryControls.ControlFactory(next_field.Fields[0], next_field)).Create());


                }
                if (result == MobileDataKit.Core.Model.Flow.FlowResult.NextField)
                {




                    throw new MobileDataKit.Core.EndOfRepeatException();


                }
               
                    
            }
        }

        public virtual object ExecutePostCondition()
        {
            return null;
        }
        public async Task<BaseControl> MoveNext(bool AddToNavigation = true)
        {

            if (ExecutePostCondition() != null)
                return null;
            try
            {

                var sddfdfdf = MobileDataKit.Core.Model.EntryForm.CurrentEntryForm;

                EntryTransaction.DefaultTransaction().CommitAndStartTransaction();


                Field next_field = this.Field;// this.Section.Fields[this.Section.Fields.IndexOf(this.Field) + 1];
                ContentPage next = null; //(new EntryControls.ControlFactory(next_field, this.Section)).Create();
                Field Current_Field = this.Field;
                ISection Current_Section = this.Section;
                next_field = Current_Section.GetNextField(Current_Section.Name);
                if (next_field != null)
                {
                    next_field.CurrentField = string.Empty;
                    //EntryForm.CurrentEntryForm.CurrentField = next_field.Name;
                    if (string.IsNullOrWhiteSpace(next_field.ControlType) || next_field.ControlType.ToLower() != "section")
                        next = (new EntryControls.ControlFactory(next_field, Current_Section)).Create();
                    else
                    {
                        if (Current_Section == null)
                            next = new EntryControls.SectionLabel(next_field);
                        else
                            next = new EntryControls.SectionLabel(next_field);
                    }
                    if (AddToNavigation)
                    {
                        await this.Navigation.PushAsync(next);

                    }

                    return (BaseControl)next;

                }
                var sectionas_field = Current_Section as Field;
                var sectionas_form = Current_Section as Form;
                Current_Section = null;
                if (sectionas_field != null)
                {
                    if (sectionas_field.ParentField != null)
                    {
                        Current_Section = sectionas_field.ParentField;
                        //   d = false;
                    }
                    else
                         if (sectionas_field.Form != null)
                    {
                        Current_Section = sectionas_field.Form;
                        // d = false;
                    }




                }
                else
                if (sectionas_form != null)
                {
                    Current_Section = sectionas_form;
                }

                if (Current_Section != null)
                {
                    var current_id = MobileDataKit.Core.Model.EntryForm.CurrentEntryForm.GetCurrentField(Current_Section.Name);
                    if (!string.IsNullOrWhiteSpace(current_id))
                    {
                        var dee = Current_Section.GetNextField(Current_Section.Name);
                        if (string.IsNullOrWhiteSpace(dee.ControlType) || dee.ControlType.ToLower() != "section")
                            next = (new EntryControls.ControlFactory(dee, Current_Section)).Create();
                        else
                        {

                            next = new EntryControls.SectionLabel(dee);
                        }
                        if (AddToNavigation)
                        {
                            await this.Navigation.PushAsync(next);
                        }

                        return (BaseControl)next;

                    }
                    var z = new EntryControls.SectionLabel(Current_Section);
                    if (AddToNavigation)
                        await this.Navigation.PushAsync(z);

                    return z;
                }






            }
            catch (Exception ex)
            {


                var answer = await DisplayAlert("Question?", "You have reached the end of the form, It is going to be ended now. Do you want to create new entry?", "Yes", "No");



                EntryForm.CurrentEntryForm.EntryStatus = "Complete";
                EntryTransaction.DefaultTransaction().Transaction.Commit();
                EntryForm.RemoveCurrentForm();
                var ddd = new ProjectMainPage(null);
                if (answer)
                {



                    var form = ddd.CreateNewEntry();

                    EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
                    Realms.Realm realm = EntryTransaction.DefaultTransaction().Realm;


                    form.CurrentField = string.Empty;
                    var dc = new EntryControls.SectionLabel(form);
                    if (AddToNavigation)
                        await this.Navigation.PushAsync(dc);
                    return dc;


                }

                else
                {
                    await this.Navigation.PushAsync(ddd);
                }


            }


            return null;
        }



        private bool GetNextAllControlInsteadOfThisPage(EntryControls.BaseControl cont)
        {


            var h = (from c in Section.Fields where c.Name == cont.ID select c).First();



          //if (!string.IsNullOrWhiteSpace(  h.DependOn))
          // {
          //      var p = new MobileDataKit_Collect.Compiler.Parser(new Compiler.Scanner());
          //  var sdsdsd=    p.Parse(h.DependOn);

          //      sdsdsd.Eval(h);

          // }




            return false;
            var ddd = new List<string>();
            ddd.Add("|");
            var depends_factor = h.Precondition.Split(ddd.ToArray(), StringSplitOptions.RemoveEmptyEntries);
          
            var cd = 0;

            var results = new List<Boolean>();

            foreach (string s in depends_factor)
            {
                try
                {
                    var s1 = s.ToString().Trim();
                    var basecontrols = (from c in Section.Fields where c.Name.ToLower() == s1.ToLower() select c).First();

                    var Corr_Objects = (from obj in EntryForm.CurrentEntryForm.EntryVariables where obj.FieldID == basecontrols.Name select obj).First();
                    //if (Corr_Objects.Value == depends_value[cd])
                    //{
                    //   return true;
                    //}
                }
                catch
                {
                    return false;
                }


                cd = cd + 1;
            }
            return false;
        }
        private bool GetNextControlInsteadOfThisPage(EntryControls.BaseControl cont)
        {


            var h = (from c in cont.Section.Fields where c.Name == cont.ID select c).First();
            if (string.IsNullOrWhiteSpace(h.Precondition))
                return false;



            //if (!string.IsNullOrWhiteSpace(h.DependOn))
            //{
            //    var p = new MobileDataKit_Collect.Compiler.Parser(new Compiler.Scanner());
            //    var sdsdsd = p.Parse(h.DependOn);
            //var qa=(new MobileDataKit_Collect.Compiler.ParserTreeEvaluator()).Eval(sdsdsd, Model.EntryForm.CurrentEntryForm, this.Section);

            //    return  !(Boolean) qa;
            //}


           
            return false;
            //var basecontrol = (from c in Section.Fields where c.Name == h.DependOn select c).First();

            //var Corr_Object = (from obj in Model.EntryForm.CurrentEntryForm.EntryVariables where obj.InternalName == basecontrol.InternalName select obj).First();
            //if (fail)
            //{
            //    var next_field = (from c in Section.Fields where c.No == h.No + 1 select c).First();



            //    foreach (EntryControls.BaseControl bas in this.Children)
            //        if (bas.ID == next_field.ID)
            //        {
            //            var ffffffffffff = GetNextControlInsteadOfThisPage(bas);
            //            if (ffffffffffff == null)
            //                return bas;
            //            else
            //                return ffffffffffff;

            //        }
            //}

            //return null;

        }


        protected EntryVariable entryVariable =null;
        public string ID { get; }
        public Field Field { get; }
        public ISection Section { get; }
        public BaseControl( Field field, ISection section)
        {
            if(field !=null)
            field.CurrentField = string.Empty;
            this.BackgroundColor = Color.White;
            if (field != null)
            {
                this.ID = field.Name;
                this.Field = field;
                Title = Field.Name;

            }
            else
                this.ID = section.Name;

                this.Section = section;
            this.Unfocused += BaseControl_Unfocused;

            var tool1 = new ToolbarItem();
            tool1.Priority = 0;
            
            tool1.Text = "Exit Entry";
            tool1.Clicked += Tool1_Clicked;
            this.ToolbarItems.Add(tool1);
            Title = Field.Name;
            tool1 = new ToolbarItem();
            tool1.Priority = 0;
            tool1.Clicked += Tool2_Clicked;
            tool1.Text = "Discard Entry";
            this.ToolbarItems.Add(tool1);




            if (field != null && field.Fields.Count ==0)
            {
                MobileDataKit.Core.Model.EntryForm.CurrentEntryForm.SetCurrentField(field.Name, Section.Name);
                if (EntryTransaction.DefaultTransaction().Realm.IsInTransaction)
                    EntryTransaction.DefaultTransaction().Commit();
                using (var tr = EntryTransaction.DefaultTransaction())
                {

                    entryVariable = field.CreateEntryVariable();
              
                    EntryTransaction.DefaultTransaction().Realm.Add(entryVariable, true);
                   
                }
                   
                 

                
            }
        }

        private async void Tool1_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Question?", "Are you sure you want to exit entry?", "Yes", "No");


            if(answer)
            {
                EntryTransaction.DefaultTransaction().Transaction.Commit();
             await  this.Navigation.PushAsync( new ProjectMainPage(ProjectInfoFormsInfo.CurrentProjectInfoFormsInfo ));
            }
        
        }


        private void Tool2_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BaseControl_Unfocused(object sender, FocusEventArgs e)
        {
            this.LeaveControl();
        }

        public virtual bool LeaveControl()
        {
            return false;

        }
    }




   
}
