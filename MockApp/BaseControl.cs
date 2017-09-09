using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MobileDataKit.Core.Model;

namespace MockApp
{
    public partial class BaseControl : UserControl
    {
        protected EntryVariable entryVariable = null;
        public string ID { get; }
        public Field Field { get; }
        public ISection Section { get; }
        public BaseControl()
        {
            InitializeComponent();
        }


        public BaseControl(Field field, ISection section)
        {
            InitializeComponent();
            if (field != null)
                field.CurrentField = string.Empty;
            
            if (field != null)
            {
                this.ID = field.Name;
                this.Field = field;

                MobileDataKit.Core.Model.EntryForm.CurrentEntryForm.SetCurrentField(field.Name, Section.Name);
            }
            else
                this.ID = section.Name;

            this.Section = section;
            




            if (field != null)
            {
                if (EntryTransaction.DefaultTransaction().Realm.IsInTransaction)
                    EntryTransaction.DefaultTransaction().Commit();
                using (var tr = EntryTransaction.DefaultTransaction())
                {

                    entryVariable = field.CreateEntryVariable();

                    EntryTransaction.DefaultTransaction().Realm.Add(entryVariable, true);

                }




            }
        }
        private void BaseControl_Load(object sender, EventArgs e)
        {

        }


        public virtual  void MoveNext()
        {


            try
            {

                
                
                EntryTransaction.DefaultTransaction().CommitAndStartTransaction();


                Field next_field = this.Field;// this.Section.Fields[this.Section.Fields.IndexOf(this.Field) + 1];
                BaseControl next = null; //(new EntryControls.ControlFactory(next_field, this.Section)).Create();
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
                        next = new EntryControls.SectionLabel(next_field);
                    this.Controls.Add(next);
                    next.BringToFront();
                    Form1.currentControl = next;
                    return;
                }
                var sectionas_field = Current_Section as Field;
                var sectionas_form = Current_Section as MobileDataKit.Core.Model.Form;
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
                    this.Controls.Add(new EntryControls.SectionLabel(Current_Section));
                    next.BringToFront();
                    Form1.currentControl = next;
                    
                }






            }
            catch (Exception ex)
            {


                EntryTransaction.DefaultTransaction().CommitAndStartTransaction();


            }


        }
    }
}
