using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileDataKit_Collect
{
    public class FormList : ContentPage
    {
        public FormList()
        {
            var lv = new ListView();

            var tem = new DataTemplate(() =>
            {

                var nameLabel = new Xamarin.Forms.Label();
                nameLabel.SetBinding(Xamarin.Forms.Label.TextProperty, "Value");

                nameLabel.TextColor = Color.Black;
                nameLabel.BackgroundColor = Color.White;

                var vc = new ViewCell();

                var st_in = new StackLayout();
                st_in.Orientation = StackOrientation.Horizontal;
                st_in.VerticalOptions = LayoutOptions.Center;
                st_in.Children.Add(nameLabel);
                vc.View = st_in;
                return vc;
            });
            lv.ItemTemplate = tem;
            var realm = Realms.Realm.GetInstance();

            var cde = new List<MobileDataKit.Core.Model.EntryVariable>();
            try
            {
                //   var tttt=  realm.All<Model.Field>().Where(f => f.Text== "Jina la mfaidishwa").First();

                var fvfvfvfvfvfvvvvvvvvvvvvvvvv = new List<MobileDataKit.Core.Model.EntryVariable>();
                Realms.Transaction tr = null;
                try
                {
                    tr = realm.BeginWrite();
                }
                catch
                {

                }
                using (tr)
                {
                    var dfdfssss = realm.All<MobileDataKit.Core.Model.EntryForm>().Where(f => f.EntryStatus == "Incomplete").ToList();


                    foreach (var i in dfdfssss)
                    {
                        try
                        {
                            var tttt = realm.All<MobileDataKit.Core.Model.EntryVariable>().Where(f => f.FieldID == "RecordNo").First();
                         

                            cde.Add(tttt);
                        }
                        catch
                        {

                        }


                    }

                    tr.Commit();
                }

                lv.ItemsSource = cde;
           

            }
            catch (Exception d)
            {
                var dfdfdfdfdf = d;
            }

            lv.ItemSelected += Lv_ItemSelected;
            this.Content = lv;
            //var textcell = new TextCell();
            //var bind = new Binding();
            //textcell.SetBinding(TextCell.TextProperty, bind);
            //lv.ItemsSource = (from g in field.FieldOptions where g.Name.Length > 0 select new SingleSelectObject((field.FieldOptions.IndexOf(g) + 1).ToString() + " . " + g.Name, g.Name)).ToList();
            //lv.ItemTemplate = tem;
            //lv.ItemSelected += Lv_ItemSelected;

        }

        private async void Lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var dfdfd = (MobileDataKit.Core.Model.EntryVariable)e.SelectedItem;


            var ddddd = dfdfd.EntryFormID;

           

            var form = App.realm.All<MobileDataKit.Core.Model.Form>().ToList()[0];
                      
        MobileDataKit.Core.Model.EntryForm.CurrentEntryForm = App.realm.All<MobileDataKit.Core.Model.EntryForm>().Where(d => d.ID == ddddd).First();


            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            Realms.Realm realm = EntryTransaction.DefaultTransaction().Realm;


            form.CurrentField = string.Empty;
            EntryControls.BaseControl control = new EntryControls.SectionLabel(form);


            




            //foreach (var r in section.Fields)
            //{
            //    if (string.IsNullOrWhiteSpace(r.FieldID))
            //    {
            //        await this.Navigation.PushAsync((new EntryControls.ControlFactory(r, section)).Create());
            //        if (Model.EntryForm.CurrentEntryForm.CurrentVariable == null)
            //            break;
            //        if (Model.EntryForm.CurrentEntryForm.CurrentVariable == r.InternalName)
            //            break;

            //    }



            //}

        }
    }
}
