using MobileDataKit.Core.Model;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDataKit_Collect
{
    [XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ProjectMainPage : ContentPage
    {



        ProjectInfoFormsInfo projectInfoFormsInfo;

        public ProjectMainPage(ProjectInfoFormsInfo _projectInfoFormsInfo)
        {
          
           InitializeComponent();
            ProjectInfoFormsInfo.CurrentProjectInfoFormsInfo = _projectInfoFormsInfo;

            projectInfoFormsInfo = _projectInfoFormsInfo;
            var ddd = (StackLayout)this.Content;
            var f = new Button();



           


            f.TextColor = Color.Black;



            var dfdfssss = App.realm.All<EntryForm>().Where(sf => sf.EntryStatus == "Complete").ToList();
            f.Text = dfdfssss.Count.ToString() + " Forms to synchronize";
            f.Clicked += F_Clicked;

            ddd.Children.Add(f);

        }

        private async void F_Clicked(object sender, EventArgs e)
        {
            // return;
            var realm = Realms.Realm.GetInstance();






            var dfdfssss = realm.All<EntryForm>().Where(sf => sf.EntryStatus == "Complete");
            var arr = Newtonsoft.Json.Linq.JArray.FromObject(new List<object>());
            foreach (var d in dfdfssss)
            {
                var jObject = Newtonsoft.Json.Linq.JObject.FromObject(new DummyObject());
                jObject.Add("formDate", Newtonsoft.Json.Linq.JToken.Parse(d.Date.ToString()));
                var client = new RestClient("http://192.168.8.101:1858/");

                var request = new RestRequest("api/PostData", Method.POST);

                //foreach(var f in dfdfssss)
                //{
                //    dddd.Add(f);
                //   f.EntryVariables
                //}



                //   ddfdfdfd.EntryVariables. = d.EntryVariables;
                request.AddJsonBody(d);
                //mbox 
                var cancellationTokenSource = new System.Threading.CancellationTokenSource();
                var response =  client.Execute(request).Result;

                var cd = response.Content;
                if (!String.IsNullOrWhiteSpace(cd))
                {
                    using (var tr = realm.BeginWrite())
                    {

                        var ds = realm.All<EntryForm>().Where(sf => sf.ID == cd).First();
                        realm.Remove(ds);
                        tr.Commit();
                    }
                }


            }


        }

        async void RefreshProject(object sender, EventArgs e)
        {
            try
            {
                RestClient client = null;

#if DEBUG
        client = new RestClient("http://192.168.8.101:1858/");
            client = new RestClient("http://192.168.43.139:1858/");
#else
            client = new RestClient("http://clouddatakit.azurewebsites.net/");
#endif

            client = new RestClient("http://clouddatakit.azurewebsites.net/");

            var request = new RestRequest("api/Form", Method.GET);

            //mbox 
            var cancellationTokenSource = new System.Threading.CancellationTokenSource();
            var response = await client.Execute(request);




            var ffffffffffff = response.Content;
            if (!string.IsNullOrWhiteSpace(ffffffffffff))

                if (App.realm.All<Form>().ToList().Count() > 0)
                {
                    using (var transaction = App.realm.BeginWrite())
                    {
                        App.realm.RemoveAll<Form>();
                        transaction.Commit();

                    }

                }
            using (var transaction = App.realm.BeginWrite())
            {
                var ddddddddddddddddddddd = Newtonsoft.Json.JsonConvert.DeserializeObject<Form>(ffffffffffff);
                // Handle when your app starts

                transaction.Commit();
            }



            }
            catch ( Exception ex)
            {
                var r = ex;

            }
            await DisplayAlert("Error", "Updated successfully", "OK");


            //    .Content;


        }


        async void IncompleteForms(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new FormList());

        }


        public Form CreateNewEntry()
        {

            EntryTransaction.DefaultTransaction().Commit();

            var dff = projectInfoFormsInfo.ID;

            var sssss = App.realm.All<Form>().Where(f=>f.ID == dff).First();

            Form.SetCurrentSection(sssss);
            using (var csssss = App.realm.BeginWrite())
            {
                sssss.SetChilds();
                Form.SetCurrentSection(sssss);
                App.realm.RemoveAll<MobileDataKit.Core.Model.CurrentFieldTrack>();
                

                try
                {
                    EntryForm.CurrentEntryForm = EntryForm.CreateNewEntry(sssss);
                    EntryForm.CurrentEntryForm.Date = DateTimeOffset.Now;
                    EntryForm.CurrentEntryForm.UserName = Plugin.DeviceInfo.CrossDevice.Hardware.DeviceId;
                    App.realm.Add(EntryForm.CurrentEntryForm, true);
                }
                catch(Exception ex)
                {
                    var sdsds = ex;

                }
                
                App.realm.Add(EntryForm.CurrentEntryForm);
                csssss.Commit();
            }


           
            return sssss;

        }
        async void OnNewProjectClicked(object sender, EventArgs e)
        {

            var form = CreateNewEntry();


            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            Realms.Realm realm = EntryTransaction.DefaultTransaction().Realm;


            form.CurrentField = string.Empty;
                await this.Navigation.PushAsync(new EntryControls.SectionLabel(form));



        }





    }


    public class DummyObject
    {




    }


}
