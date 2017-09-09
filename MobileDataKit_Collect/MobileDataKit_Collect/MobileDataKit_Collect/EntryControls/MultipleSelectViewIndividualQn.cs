using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace MobileDataKit_Collect.EntryControls
{
	public class MultipleSelectViewIndividualQn : MultipleSelectViewBase
    {


        private List<BaseControl> _List = new List<BaseControl>();

        public MultipleSelectViewIndividualQn(Field field ,ISection section) : base( field, section)
        {
            var st = new StackLayout();

            var f = new Xamarin.Forms.Label();
            f.Text = field.Text;
            f.TextColor = Color.Black;
            f.BackgroundColor = Color.White;
            st.Children.Add(f);


            f = new Xamarin.Forms.Label();
            f.Text = "Multi select question " +  field.Fields.Count.ToString() + " Options ";
            f.TextColor = Color.Black;
            f.BackgroundColor = Color.White;
            st.Children.Add(f);


            var b = new Button();
            b.Text = "Next";
            b.TextColor = Color.Black;
            b.BackgroundColor = Color.White;
            b.Clicked += Next_button_Clicked;
            st.Children.Add(b);










            var ro = 1;
            foreach (var dx in field.Fields)
            {
                

                EntryVariable child_variable = null;
                var dd = dx.Name;

                if (EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == dd).Count() > 0)
                    child_variable = EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == dd).First();
                else
                {
                    child_variable = new EntryVariable();// EntryTransaction.DefaultTransaction().Realm.CreateObject<EntryVariable>();
                    child_variable.FieldID = dd;
                    EntryTransaction.DefaultTransaction().Realm.Add(child_variable);
                    EntryForm.CurrentEntryForm.EntryVariables.Add(child_variable);
                }

                var ddfdfdfd = (new ControlFactory (dx, this.Section)).Create();
                ddfdfdfd.nextstepevent += Ddfdfdfd_nextstepevent;
                var ll = new Xamarin.Forms.Label();
                ll.Text = field.Text;
                ll.BackgroundColor = Color.White;
                ll.TextColor = Color.Black;
                ll.Text =ro.ToString() +" ." + field.Text;
                ((StackLayout)ddfdfdfd.Content).Children.Insert(0, ll);
                ro = ro + 1;

                ll = new Xamarin.Forms.Label();
                ((StackLayout)ddfdfdfd.Content).Children.Insert(1, ll);

                ll = new Xamarin.Forms.Label();
                ((StackLayout)ddfdfdfd.Content).Children.Insert(1, ll);

                _List.Add(ddfdfdfd);


            }

            this.Content = st;
        }

        private void Ddfdfdfd_nextstepevent(object obj, NextStepEventArgs e)
        {

            e.Handled = true;
            try
            {
                current_step = _List[_List.IndexOf(current_step) + 1];
                this.Navigation.PushAsync(current_step);
            }
            catch
            {
                MoveNext();
            }
          
        }

        public BaseControl current_step = null;
        private void Next_button_Clicked(object sender, EventArgs e)
        {
            current_step = _List[0];
            this.Navigation.PushAsync(_List[0]);


            //bool has_value = false;
            //foreach (var dx in Field.Fields)
            //{
            //    var dd = dx.InternalName;


            //    var child_variable = Model.EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.InternalName == dd).First();

            //    if (!string.IsNullOrWhiteSpace(child_variable.Value))
            //        has_value = true;
            //}


            //if (has_value == false)
            //{
            //   DisplayAlert("Error", "Enter Text please", "OK");
            //  return;
            //}


            //this.MoveNext();


        }

        public override bool LeaveControl()
        {

           

          

            return true;
        }
    }

   






 
}
