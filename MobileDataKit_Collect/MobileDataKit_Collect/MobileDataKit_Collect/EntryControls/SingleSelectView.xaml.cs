using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace MobileDataKit_Collect.EntryControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleSelectView : BaseControl
    {
        


        public SingleSelectView(Field field, ISection section) : base( field, section)
        {
            InitializeComponent();


            lblText.Text = field.Text;
            // lb.FontSize = 50;

            

            //var textcell = new TextCell();

            var vn = (from g in field.FieldOptions where g.Name.Length > 0 select new SingleSelectObject((field.FieldOptions.IndexOf(g) + 1).ToString() + " . " + g.Name, g.Name)).ToList();
            
            lv.ItemsSource = vn;
            //textcell.SetBinding(TextCell.TextProperty, bind);
            var dc = new ssdsds();
            dc.SingleSelectObjects = vn;
            var bind = new Binding("Value", BindingMode.TwoWay, new SingleSelectValueConverter(), dc);
            lv.SetBinding(ListView.SelectedItemProperty, bind);
            lv.BindingContext = entryVariable;

            
            lv.ItemSelected += Lv_ItemSelected;
           
        }




        private void Lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (entryVariable.Value == null)
            {
                this.DisplayAlert("Error", "Please choose option", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(entryVariable.Value))
            {
                this.DisplayAlert("Error", "Please choose option", "OK");
                return;
            }
            var args = new NextStepEventArgs();
            OnNextStepEvent(args);

            if (!args.Handled)
                this.MoveNext();
        }

        public override bool LeaveControl()
        {

            ListView vc = (ListView)((StackLayout)this.Content).Children[1];


            if (vc.SelectedItem == null)
            {
                DisplayAlert("Error", "Choose a selection please", "OK");
                return false;
            }



            return true;
        }





    }




    public class NextStepEventArgs : EventArgs
    {
        public Boolean Handled { get; set; }
    }




    public class ssdsds
    {
        public List<SingleSelectObject> SingleSelectObjects { get; set; }

    }

    public class SingleSelectObject
    {
        public string Name { get; set; }

        public string Text {get;set;}

       
        public SingleSelectObject(string text,string name)
        {
            this.Name = name;
            this.Text = text;
            
        }
    }

    public class SingleSelectValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return null;
            var cd = (ssdsds)parameter;
            var v = cd.SingleSelectObjects;


            try
            {

                return v[int.Parse(value.ToString().Trim()) - 1];
            }
            catch (Exception dc)
            {
                var ds = dc;

            }


            return null;
        }

       

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var cd = (ssdsds)parameter;
            var v = cd.SingleSelectObjects;

            var cdcdcdcd = (SingleSelectObject)value;
            var d = 1;
            foreach (var b in v)
            {
                if (b.Name == cdcdcdcd.Name)
                    return d;

                d = d + 1;
            }

            return null;
        }

       
    }
}