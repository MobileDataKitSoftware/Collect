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
    public class VariableUploadService : Service
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
           
            



            return StartCommandResult.Sticky;

        }
    }
}
