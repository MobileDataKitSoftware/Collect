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
using Virgil.SDK.Device;
[assembly: Xamarin.Forms.Dependency(typeof(MobileDataKit_Collect.Droid.Encryption.DeviceManager))]
namespace MobileDataKit_Collect.Droid.Encryption
{
    public class DeviceManager : Virgil.SDK.Device.IDeviceManager
    {
        public DeviceManager()
        {

        }
        string IDeviceManager.GetDeviceModel()
        {
            return "android";
        }

        string IDeviceManager.GetDeviceName()
        {
           return Xamarin.Forms.Device.Android;
        }

        string IDeviceManager.GetSystemName()
        {
           return "android";
        }

        string IDeviceManager.GetSystemVersion()
        {
            return "android";
        }
    }
}