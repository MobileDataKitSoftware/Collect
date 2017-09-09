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
	public class MultipleSelectView : MultipleSelectViewBase
    {


        public MultipleSelectView(Field field , Field section) : base( field, section)
        {
            var st = new StackLayout();
            
            var lb = new Xamarin.Forms.Label();
            lb.Text = field.Text;
            st.Orientation = StackOrientation.Vertical;
            lb.TextColor = Color.Black;
          //  lb.FontSize = 50;
            var label= new Xamarin.Forms.Label();
            //label.FontSize = 50;
            label.TextColor = Color.Black;
            label.Text = field.Text;

            st.Children.Add(label);

            var co = new Grid();
           
           
            co.ColumnDefinitions.Add(new ColumnDefinition());
            co.ColumnDefinitions.Add(new ColumnDefinition());
            var ro = 0;
            foreach (var dx in field.Fields)
            {
                

                EntryVariable child_variable = null;
                var dd = dx.Name;

                if (EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == dd).Count() > 0)
                    child_variable = EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == dd).First();
                else
                {
                    child_variable = new EntryVariable();
                    child_variable.FieldID = dd;
                    EntryTransaction.DefaultTransaction().Realm.Add(child_variable);
                   EntryForm.CurrentEntryForm.EntryVariables.Add(child_variable);
                }
                var bo = true;
                //if(!string.IsNullOrWhiteSpace(dx.DependOn))
                //{
                //    var p = new MobileDataKit_Collect.Compiler.Parser(new Compiler.Scanner());
                //    var sdsdsd = p.Parse(dx.DependOn);
                //    var qa = (new MobileDataKit_Collect.Compiler.ParserTreeEvaluator()).Eval(sdsdsd, Model.EntryForm.CurrentEntryForm, this.Section);

                // bo= !(Boolean)qa;

                //}
                if (bo)
                {


                    co.RowDefinitions.Add(new RowDefinition());
                    var l = new Xamarin.Forms.Label();
                    l.TextColor = Color.Black;
                    l.HorizontalOptions = LayoutOptions.Start;
                    l.Text = dx.Text;
                    Grid.SetColumn(l, 0);
                    Grid.SetRow(l, ro);
                    co.Children.Add(l);
                    View l2 = null;
                    var has_field_options = false;
                    try
                    {
                        has_field_options = (dx.FieldOptions.Where(f => f.Name.Length > 0).Count() > 0);
                    }
                    catch
                    {

                    }

                    var control_type_specified = false;

                    try
                    {
                        control_type_specified = (dx.ControlType.Length > 0);
                    }
                    catch
                    {

                    }

                    if (has_field_options)
                    {
                        Picker picker = new Picker
                        {
                            Title = "Choose",

                            VerticalOptions = LayoutOptions.CenterAndExpand
                        };






                        picker.BackgroundColor = Color.Gray;
                        picker.TextColor = Color.Black;
                        foreach (var d in dx.FieldOptions)
                            picker.Items.Add(d.Name);
                        var bind = new Binding("Value", BindingMode.TwoWay, new PickerValueConverter(), dx.FieldOptions);
                        picker.SetBinding(Picker.SelectedIndexProperty, bind);
                        picker.BindingContext = child_variable;
                        Grid.SetColumn(picker, 1);
                        Grid.SetRow(picker, ro);
                        co.Children.Add(picker);
                    }
                    else if (!control_type_specified)
                    {
                        Switch switcher = new Switch
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.CenterAndExpand
                        };


                        //   switcher.Toggled += Switcher_Toggled;
                        switcher.BindingContext = child_variable;
                        var bind = new Binding("Value", BindingMode.TwoWay, new SwitchValueCpnverter());

                        switcher.SetBinding(Switch.IsToggledProperty, bind);
                        Grid.SetColumn(switcher, 1);
                        Grid.SetRow(switcher, ro);
                        co.Children.Add(switcher);
                    }
                    else
                    {
                        l2 = new Xamarin.Forms.Entry();
                        ((Xamarin.Forms.Entry)l2).TextColor = Color.Black;
                        var bind = new Binding("Value", BindingMode.TwoWay);

                        l2.SetBinding(Xamarin.Forms.Entry.TextProperty, bind);
                        //  l2.MinimumWidthRequest = 300;
                        ((Xamarin.Forms.Entry)l2).Placeholder = "Enter Value";
                        ((Xamarin.Forms.Entry)l2).PlaceholderColor = Color.Black;
                        l2.BindingContext = child_variable;
                        l2.HorizontalOptions = LayoutOptions.CenterAndExpand;
                        var cdd = new Frame();
                        cdd.Content = l2;
                        cdd.OutlineColor = Color.Black;

                        Grid.SetColumn(l2, 1);
                        Grid.SetRow(l2, ro);
                        co.Children.Add(l2);



                    }
                    //   Xamarin.
                    // co.Content = ;

                    ro = ro + 1;
                }
            }
            
            st.Children.Add(co);

            StackLayout st_footer = new StackLayout();
            Button next_button = new Button();
            next_button.Text = "Next";
            next_button.TextColor = Color.Black;
            next_button.Clicked += Next_button_Clicked;

            st_footer.Children.Add(next_button);
            st.Children.Add(st_footer);

            this.Content = st;
        }

        private void Next_button_Clicked(object sender, EventArgs e)
        {
            bool has_value = false;
            foreach (var dx in Field.Fields)
            {
                var dd = dx.Name;


                var child_variable =EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == dd).First();

                if (!string.IsNullOrWhiteSpace(child_variable.Value))
                    has_value = true;
            }


            if (has_value == false)
            {
               DisplayAlert("Error", "Enter Text please", "OK");
              return;
            }


            this.MoveNext();


        }

        public override bool LeaveControl()
        {

           

          

            return true;
        }
    }

   
}
