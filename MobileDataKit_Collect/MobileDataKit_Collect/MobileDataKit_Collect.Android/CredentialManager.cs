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
using MobileDataKit_Collect.Auth;
using Xamarin.Auth;
using Xamarin.Forms;

namespace MobileDataKit_Collect.Droid
{
    public class CredentialManager : Auth.ICredentialManager
    {
        void ICredentialManager.SaveCredentials(string userName, string password, string token)
        {
            Plugin.SecureStorage.SecureStorageImplementation.StorageFile = userName.ToLower();
            Plugin.SecureStorage.SecureStorageImplementation.StoragePassword = password;
            if (Plugin.SecureStorage.CrossSecureStorage.Current.HasKey("Password"))
                Plugin.SecureStorage.CrossSecureStorage.Current.DeleteKey("Password");
            if (Plugin.SecureStorage.CrossSecureStorage.Current.HasKey("token"))
                Plugin.SecureStorage.CrossSecureStorage.Current.DeleteKey("token");
         



            Plugin.SecureStorage.CrossSecureStorage.Current.SetValue("Password", password);
            Plugin.SecureStorage.CrossSecureStorage.Current.SetValue("token", token);
            if (!Plugin.SecureStorage.CrossSecureStorage.Current.HasKey("UserReam"))
                Plugin.SecureStorage.CrossSecureStorage.Current.SetValue("UserReam", Guid.NewGuid().ToString());

            SecureValues.UserName = userName;
        }
    }
}