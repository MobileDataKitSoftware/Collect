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
    public partial class SectionLabel : BaseControl
    {
       


        public string SectionName { get; set; }

        public SectionLabel(ISection section) : base(null,section)
        {
            InitializeComponent();
           
           
            lb.Text = section.Text;
            if (string.IsNullOrWhiteSpace(lb.Text))

                lb.Text = section.Name;

            lb.TextColor = Color.Black;

            lb.BackgroundColor = Color.White;     
                        
            bu.Clicked += Bu_Clicked;
            bu.Text = "Next";
            bu.TextColor = Color.Black;
            bu.BackgroundColor = Color.White;
           

            var section_field = section as Field;

            if(section_field !=null)
            {
                if (section_field.ParentField != null)
                    SectionName = section_field.ParentField.Name;
                else
                    if (section_field.Form != null)
                    SectionName = section_field.Form.Name;
            }
           
                MobileDataKit.Core.Model.EntryForm.CurrentEntryForm.SetCurrentField(section.Name, SectionName);
               

            
           

        }

        private void Bu_Clicked(object sender, EventArgs e)
        {
            this.MoveNext();
        }

        private void Next_button_Clicked(object sender, EventArgs e)
        {





            this.MoveNext();


        }

        public override bool LeaveControl()
        {





            return true;
        }
    }
}