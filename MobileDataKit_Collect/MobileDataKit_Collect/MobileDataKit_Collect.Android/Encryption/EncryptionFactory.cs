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
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(MobileDataKit_Collect.Droid.Encryption.EncryptionFactory))]
namespace MobileDataKit_Collect.Droid.Encryption
{
    public class EncryptionFactory : MobileDataKit_Collect.Encryption.IEncryptionFactory
    {
        public string Encrypt(object Data, List<MobileDataKit.Core.Model.EndPoints.RemoteFormEndPoint> cards)
        {

           
            var encryptedObject = new MobileDataKit.Core.Model.EncryptedObject();



            var TDES = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
           TDES.GenerateIV();
            TDES.GenerateKey();
           string Data_Text= Newtonsoft.Json.JsonConvert.SerializeObject(Data);
            using (var stream = new System.IO.MemoryStream())
            {
                using (var CryptStream = new CryptoStream(stream, TDES.CreateEncryptor(), CryptoStreamMode.Write))
                {

                    //Create a StreamWriter for easy writing to the   
                    //network stream.
                    StreamWriter SWriter = new StreamWriter(CryptStream);

                    //Write to the stream.  
                    SWriter.WriteLine(System.Text.UTF8Encoding.UTF8.GetBytes(Data_Text));

                    //Inform the user that the message was written  
                    //to the stream.  
                    //Console.WriteLine("The message was sent.");

                    //Close all the connections.  
                    SWriter.Close();
                    CryptStream.Close();
                }

                encryptedObject.Data = BitConverter.ToString(stream.ToArray());
            }
            //encryptedObject.Data = aliceCard.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(Data)).ToString(StringEncoding.Base64);
            
            
          
            foreach(var c in cards)
            {
                var rec = new MobileDataKit.Core.Model.EncryptedObjectRecepient();



                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(c.PublicKey);
                //Create a new instance of the RSAParameters structure.  
               // RSAParameters RSAKeyInfo = new RSAParameters();
               //RSA.
               // //Set RSAKeyInfo to the public key values.   
               // RSAKeyInfo.Modulus = PublicKey;
               // RSAKeyInfo.Exponent = Exponent;

                //Import key parameters into RSA.  
                //RSA.ImportParameters(RSAKeyInfo);
                rec.Key = new MobileDataKit.Core.Model.EncryptedKey() { Key = BitConverter.ToString( RSA.Encrypt(TDES.Key, true)), IV =  BitConverter.ToString( RSA.Encrypt(TDES.Key, true)) } ;
                rec.UserID = c.UserID;
                rec.DeviceID = c.DeviceID;
                rec.RecepientKey = c.PrivateKeyID;
                encryptedObject.Recepients.Add(rec);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(encryptedObject);
        }
    }
}