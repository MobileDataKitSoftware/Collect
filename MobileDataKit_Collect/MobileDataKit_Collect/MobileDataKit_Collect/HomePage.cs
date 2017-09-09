using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileDataKit_Collect
{
    public class HomePage : MasterDetailPage
    {
        public  HomePage()
        {
            Label header = new Label
            {
                Text = "MasterDetailPage",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            using (var client2 = new HttpClient())
            {
                client2.BaseAddress = new Uri(Model.AppConstants.Url);
                

                var response2 = client2.GetAsync("api/ProjectMeta").Result;


                var sdssd =  response2.Content.ReadAsStringAsync().Result ;
                var gg = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MobileDataKit.Core.Model.Project>>(sdssd);

                foreach(var pr in gg)
                {
                    foreach(var form in pr.Forms)
                    {
                        form.Project = pr;
                        foreach(var ls in form.Fields)
                        {
                            ls.Form = form;
                            ls.SetChilds();
                        }
                    }
                }
               
                  using(var tr =  App.realm.BeginWrite())
                  {
                    foreach (var pr in gg)
                    {
                        App.realm.Add(pr, true);
                      

                    }
                    tr.Commit();
                }

                using (var tr = App.realm.BeginWrite())
                {
                    foreach (var pr in gg)
                    {

                       foreach(var e in pr.Forms)
                        { 

                            var enc = DependencyService.Get<MobileDataKit_Collect.Encryption.IEncryptionKeyGenerator>();


                            var f = new MobileDataKit.Core.Model.LocalFormCard();
                            f.ID = e.ID;
                            f.Card =  enc.Generate(e.ID);
                           
                                App.realm.Add(f,true);
                           

                        }

                    }
                    tr.Commit();
                }

            }

            var projects = new List<ProjectInfo>();
            foreach (var pr in App.realm.All<MobileDataKit.Core.Model.Project>()) //;// select new ProjectInfo(t.ProjectName)).ToList();
            {
                var pr1 = new ProjectInfo(pr.ProjectName);
                foreach(var t in pr.Forms)
                {
                    var forminfo = new ProjectInfoFormsInfo();
                    forminfo.ID = t.ID;
                    forminfo.Name = t.Name;
                    pr1.Forms.Add(forminfo);
            

                 
                }
                projects.Add(pr1);
            }
            // Assemble an array of NamedColor objects.
            

            // Create ListView for the master page.
            ListView listView = new ListView
            {
                ItemsSource = projects
            };

            // Create the master page with the ListView.
            this.Master = new ContentPage
            {
                Title = header.Text,
                Content = new StackLayout
                {
                    Children =
                    {
                        header,
                        listView
                    }
                }
            };

            // Create the detail page using NamedColorPage and wrap it in a
            // navigation page to provide a NavigationBar and Toggle button
          /// this.Detail = new NavigationPage(new NamedColorPage(true));
            this.Detail = new NavigationPage(new FormList());
            // For Windows Phone, provide a way to get back to the master page.
            if (Device.OS == TargetPlatform.WinPhone)
            {
                (this.Detail as ContentPage).Content.GestureRecognizers.Add(
                    new TapGestureRecognizer((view) =>
                    {
                        this.IsPresented = true;
                    }));
            }

            // Define a selected handler for the ListView.
            listView.ItemSelected += (sender, args) =>
            {
                // Set the BindingContext of the detail page.
                this.Detail.BindingContext = args.SelectedItem;

                // Show the detail page.
                this.IsPresented = false;
            };

            // Initialize the ListView selection.
            listView.SelectedItem = projects[0];


        }
    }
}
