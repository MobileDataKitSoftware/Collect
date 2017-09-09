using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Android.Content;

namespace MobileDataKit_Collect.Droid
{
    [Activity(Label = "MobileDataKit_Collect", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Websockets.Droid.WebsocketConnection.Link();
            MessagingCenter.Subscribe<MobileDataKit_Collect.DataUpload.StartBatchDataUpload>(this, "StartBatchDataUpload", message => {
                //var intent = new Intent(this, typeof(SessionUploadService));
                //StartService(intent);
            });


            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Auth.Manager.CredentialManager = new CredentialManager();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

