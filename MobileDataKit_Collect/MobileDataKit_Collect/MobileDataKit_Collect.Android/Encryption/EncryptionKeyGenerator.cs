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
using System.Security.Cryptography;

[assembly: Xamarin.Forms.Dependency(typeof(MobileDataKit_Collect.Droid.Encryption.EncryptionKeyGenerator))]
namespace MobileDataKit_Collect.Droid.Encryption
{
  public  class EncryptionKeyGenerator : MobileDataKit_Collect.Encryption.IEncryptionKeyGenerator
    {


        public byte[] GetKey(string Password, string username)
        {
          
            var password = System.Text.Encoding.UTF8.GetBytes(Password);
            var salt = System.Text.Encoding.ASCII.GetBytes(username);
            Rfc2898DeriveBytes a = new Rfc2898DeriveBytes(Password, salt,1000);
            //return a.CryptDeriveKey("AES", "SHA1", 64,);
            return a.GetBytes(64);
          //  return a.CryptDeriveKey("RC4", "SHA1", 64,password);
        }
        public string Generate(string name)
        {


            var RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
            //Save the public key information to an RSAParameters structure.  
            RSAParameters RSAKeyInfo = RSA.ExportParameters(false);

           
          (new DefaultKeyStorage()).Store(new KeyEntry() { Name = name, Value = System.Text.UTF8Encoding.UTF8.GetBytes(RSA.ToXmlString(true)) });

            return RSA.ToXmlString(false);

        }
    }
}