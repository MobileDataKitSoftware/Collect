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
    public partial class MultipleSelectFieldList : BaseControl
    {


        public MultipleSelectFieldList(Field field, ISection section) : base(field, section)
        {
            InitializeComponent();
            lb.Text = field.Text;

            var items = new List<Model.MultipleSelectFieldListItem>();

            MobileDataKit.Core.Model.EntryForm.CurrentEntryForm.SetCurrentField(field.Name, Section.Name);
            if (EntryTransaction.DefaultTransaction().Realm.IsInTransaction)
                EntryTransaction.DefaultTransaction().Commit();
            using (var tr = EntryTransaction.DefaultTransaction())
            {

                foreach (var field1 in field.Fields)
                {
                    entryVariable = field1.CreateEntryVariable();

                    EntryTransaction.DefaultTransaction().Realm.Add(entryVariable, true);
                    items.Add(new Model.MultipleSelectFieldListItem(entryVariable, field1.Text));
                }

                

            }
            
            listView.ItemsSource = items;


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

            foreach (var field1 in Field.Fields)
            {
                var ffffff = (from v in MobileDataKit.Core.Model.EntryForm.CurrentEntryForm.EntryVariables where v.FieldID == field1.Name select v).First();

           
                    if(ffffff.Value !=null && !string.IsNullOrWhiteSpace(ffffff.Value.ToString()))
                {
                    lblError.IsVisible = false;
                    return null;
                }
                    
                    }
            
                ShowError("Please choose option");
                return 8;
            





          
            
            lblError.IsVisible = false;
            return null;

        }
        private async Task btnMoveNext_ClickedAsync(object sender, EventArgs e)
        {
            await this.MoveNext();
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
        }
    }
}