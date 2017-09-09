using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Auth;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(MobileDataKit_Collect.Droid.UserStore))]
namespace MobileDataKit_Collect.Droid
{
    public class UserStore : Auth.IUserStore
    {
        public string PasswordHash { get; set; }
     public   string UserReam { get; set; }
        public void RefreshValue(string username, string password)
        {

            Plugin.SecureStorage.SecureStorageImplementation.StorageFile = username.ToLower();
            Plugin.SecureStorage.SecureStorageImplementation.StoragePassword = password;


            if (Plugin.SecureStorage.CrossSecureStorage.Current.HasKey("Password"))
                PasswordHash = Plugin.SecureStorage.CrossSecureStorage.Current.GetValue("Password");
            if (Plugin.SecureStorage.CrossSecureStorage.Current.HasKey("UserReam"))
                UserReam = Plugin.SecureStorage.CrossSecureStorage.Current.GetValue("UserReam");
          
               

            }


        
    }
}