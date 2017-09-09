using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDataKit_Collect.EntryControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextBoxView : BaseControl
    {
        



        public TextBoxView(Field field, ISection section) :base( field, section)
        {
            InitializeComponent();

          
            var dfdfd = new StackLayout();
            var l = new Xamarin.Forms.Label();
            //  l.FontSize = 50;
        lblText.Text = field.Text;
            dfdfd.Children.Add(l);

            l.TextColor = Color.Black;


          
            if (!string.IsNullOrWhiteSpace(field.ControlType))
                if (field.ControlType.Trim().ToLower() == "numeric")
                    txtEntry.Keyboard = Keyboard.Numeric;




            var bind = new Binding("Value", BindingMode.TwoWay);

            txtEntry.SetBinding(Xamarin.Forms.Entry.TextProperty, bind);
            txtEntry.TextChanged += TxtEntry_TextChanged;
            txtEntry.Completed += L2_Completed;
            txtEntry.BindingContext = entryVariable;
            txtEntry.TextColor = Color.Black;
            lblError.IsVisible = false;
            lblError.TextColor = Color.Red;
        }


        private void ShowError(string error)
        {
            lblError.Text = error;
            lblError.IsVisible = true;
        }
        public override object ExecutePostCondition()
        {
            if (string.IsNullOrWhiteSpace(txtEntry.Text))
            {
                ShowError("Please enter text");
                return 8;
            }
                
            if (string.IsNullOrWhiteSpace(Field.PostCondition))
            {
                lblError.IsVisible = false;
                return null;
                
            }
                



            var parser = DependencyService.Get<MobileDataKit.Core.Model.Flow.IParser>();
            var res = parser.ParseExpression(Field.PostCondition).Eval(EntryForm.CurrentEntryForm, Field);

            try
            {
                if (bool.Parse(res.ToString()))

                {
                    ShowError("Umri usiwe miaka" + txtEntry.Text);
                    return 7;
                }
            }
            catch
            {

            }
            lblError.IsVisible = false;
            return null;

        }

        private void TxtEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((Xamarin.Forms.Entry)sender).Text = e.NewTextValue.ToUpper();
        }

        private void L2_Completed(object sender, EventArgs e)
        {
            var dfdfdfd = (Xamarin.Forms.Entry)sender;
            if (this.Field.Required && string.IsNullOrWhiteSpace(dfdfdfd.Text))
            {
                this.DisplayAlert("Error", "Please Enter Text", "OK");
                return;
            }
            var args = new NextStepEventArgs();
            OnNextStepEvent(args);

            if (!args.Handled)
                this.MoveNext();
        }

        public override bool LeaveControl()
        {

            var vc1 = (Xamarin.Forms.Frame)((StackLayout)this.Content).Children[1];
            var vc = (Xamarin.Forms.Entry)vc1.Content;

            if (this.Field.Required && string.IsNullOrWhiteSpace(vc.Text))
            {
                DisplayAlert("Error", "Enter Text please", "OK");
                return false;
            }




            return true;
        }
    }
}