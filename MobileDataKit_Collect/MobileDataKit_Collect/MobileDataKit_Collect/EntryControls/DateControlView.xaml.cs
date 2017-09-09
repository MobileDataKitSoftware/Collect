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
    public partial class DateControlView : BaseControl
    {
        


        public DateControlView(Field field, ISection section) :base( field, section)
        {
            InitializeComponent();



           
            
            //  l.FontSize = 50;
            
            lb.Text = field.Text;


            this.BindingContext = entryVariable;
        }

        private async void DatePicker_DateSelectedAsync(object sender, DateChangedEventArgs e)
        {
         await   MoveNext();

        }
    }
}