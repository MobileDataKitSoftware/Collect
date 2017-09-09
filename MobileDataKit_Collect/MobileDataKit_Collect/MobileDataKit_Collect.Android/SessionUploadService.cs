using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable;

namespace MobileDataKit_Collect
{
    [Service]
    public class SessionUploadService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        public override void StartActivity(Intent intent)
        {
           
        }


        [return: GeneratedEnum]
        public override  StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            var url = intent.GetStringExtra("url");

            var fff = App.realm.All<Model.EntryVariable>().Where(a => a.Synced == false).ToList();


            foreach (var variable in fff)
            {
                var client = new RestClient("http://clouddatakit.azurewebsites.net/");

                var request = new RestRequest("api/VariableData", Method.POST);
                request.AddBody(variable);

              var response=   client.Execute<string>(request).Result;
                 
               

            }




            return StartCommandResult.Sticky;

        }
    }
}
