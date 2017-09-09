using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit_Collect.Notification
{
  public  class NotificationManager
    {

        
            static NotificationManager defaultInstance = new NotificationManager();
            MobileServiceClient client;


            private NotificationManager()
            {
                this.client = new MobileServiceClient(
                    Constants.ApplicationURL);

            
            }

            public static NotificationManager DefaultManager
            {
                get
                {
                if (defaultInstance == null)
                    defaultInstance = new NotificationManager();

                    return defaultInstance;
                }
                private set
                {
                    defaultInstance = value;
                }
            }

            public MobileServiceClient CurrentClient
            {
                get { return client; }
            }

           


        
    }
}
