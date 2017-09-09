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
using Virgil.SDK.Storage;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(MobileDataKit_Collect.Droid.Encryption.DefaultKeyStorage))]
namespace MobileDataKit_Collect.Droid.Encryption
{
    public class DefaultKeyStorage 
    {
       
        public  DefaultKeyStorage()
        {

        }
        public void Delete(string keyName)
        {
            if (!this.Exists(keyName))
                throw new KeyEntryNotFoundException();

            var account = AccountStore.Create(Forms.Context).FindAccountsForService(keyName).First();
           
            
            AccountStore.Create(Forms.Context).Delete(account, keyName);
        }

        public bool Exists(string keyName)
        {
           


            var service = AccountStore.Create(Forms.Context).FindAccountsForService(keyName).ToList();
            if (service.Count() == 0)
                return false;

            return true;
            
        }

        public KeyEntry Load(string keyName)
        {
            if (!this.Exists(keyName))
            {
                throw new KeyEntryNotFoundException();
            }

            var keyEntryType = new
            {
                value = new byte[] { },
                meta_data = new Dictionary<string, string>()
            };


            var account = AccountStore.Create(Forms.Context).FindAccountsForService(keyName).First();

            // (entry.Name, Encoding.UTF8.GetString(keyEntryCipher));
            var encryptedData = Encoding.UTF8.GetBytes(account.Properties["value"]);
            var data = System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
            var keyEntryJson = Encoding.UTF8.GetString(data);

            var keyEntryObject = JsonConvert.DeserializeAnonymousType(keyEntryJson, keyEntryType);

            return new KeyEntry
            {
                Name = keyName,
                Value = keyEntryObject.value,
                MetaData = keyEntryObject.meta_data
            };
        }

        public void Store(KeyEntry entry)
        {


            

            if (this.Exists(entry.Name))
            {
                throw new Virgil.SDK.Exceptions.KeyEntryAlreadyExistsException();
            }

            var keyEntryJson = new
            {
                value = entry.Value,
                meta_data = entry.MetaData
            };

            var keyEntryData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(keyEntryJson));
            var keyEntryCipher = keyEntryData;// System.Security.Cryptography.ProtectedData.Protect(keyEntryData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);


            Account account = new Account
            {
                Username = SecureValues.UserName 
            };

            account.Properties.Add("value", Encoding.UTF8.GetString(keyEntryCipher));
        
            AccountStore.Create(Forms.Context).Save(account, entry.Name);

        }


        
    }



    public class KeyEntryNotFoundException : Exception
    {

    }
    public class KeyEntry
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the key pair.
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// Gets or sets the meta data associated with key pair.
        /// </summary>
        public Dictionary<string, string> MetaData { get; set; } = new Dictionary<string, string>();

    }
}