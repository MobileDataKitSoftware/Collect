using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Realtime
{
   public class FakeRealtimeVariableSocket : RealtimeVariableSocket
    {


        protected override void Connect()
        {
           
        }

        public override async Task UploadDataAsync()
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected && await Plugin.Connectivity.CrossConnectivity.Current.IsRemoteReachable(Model.AppConstants.Host, Model.AppConstants.Port))
                RealtimeVariableSocket.instance = null;
        }
    }
}
