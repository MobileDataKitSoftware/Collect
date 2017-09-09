using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MobileDataKit_Collect
{
    public partial class App : Application
    {

        public static bool IsUserLoggedIn = false;
        
        public static Realms.Realm realm = null;
        public App()
        {
            InitializeComponent();

           

          

            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
               
                
                //page.DisplayAlert("Connectivity Changed", "IsConnected: " + Plugin.Connectivity.CrossConnectivity.Current.IsConnected + "  Args:" + args.IsConnected.ToString(), "OK");
            };

            var message = new DataUpload.StartBatchDataUpload();
            MessagingCenter.Send(message, "StartBatchDataUpload");




            ////var client = new RestSharp.RestClient("http://192.168.8.100:1858/");
            var myNavPage = new NavigationPage(new LoginPage());
            myNavPage.Popped += MyNavPage_Popped;
            MainPage = myNavPage;
            return;
        }

        private void MyNavPage_Popped(object sender, NavigationEventArgs e)
        {

            var base_control = e.Page as EntryControls.BaseControl;
            if(base_control !=null)
            {
                var sectionid = string.Empty;
                var fieldname = string.Empty;
                if (base_control.Field != null)
                {
                    if (base_control.Field.Form != null)
                        sectionid = base_control.Field.Form.ID;
                    else
                if (base_control.Field.ParentField != null)
                        sectionid = base_control.Field.ParentField.Name;


                    fieldname = base_control.Field.Name;
                }
                else
                    sectionid = base_control.Section.Name;
                
                if(typeof(EntryControls.SectionLabel) ==e.Page.GetType())
                {
                    var section_control = e.Page as EntryControls.SectionLabel;
                    sectionid = section_control.SectionName;
                    fieldname = base_control.Section.Name;

                }
               
                MobileDataKit.Core.Model.EntryForm.CurrentEntryForm.RemoveCurrentField(fieldname, sectionid);
            }
               



        }

        private  void Connection_OnClosed()
        {
          
         //   Debug.WriteLine("Closed !");
        }
        private void Connection_OnError(string obj)
        {
           // _failed = true;
          //  Debug.WriteLine("ERROR " + obj);
        }

        private void Connection_OnOpened()
        {
           // Debug.WriteLine("Opened !");
        }

        private void Connection_OnMessage(string obj)
        {
            //_echo = true;
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    ReceivedData.Children.Add(new Label { Text = obj });
            //});
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
