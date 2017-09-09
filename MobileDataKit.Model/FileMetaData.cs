using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MobileDataKit.Model
{
public    class FileMetaData


    {

        public string FormID { get; set;  }
        private string _SafeData;
        public string SafeData
        {
            get
            {
                if(string.IsNullOrWhiteSpace(_SafeData))
                    _SafeData = Data.Substring(Data.IndexOf(",") + 1);
                return _SafeData;
            }
        }

        public string FileType { get; set; }
        public byte[] ByteArray
        {
            get
            {
                return System.Convert.FromBase64String(SafeData);
            }
        }


        public string ComputeFileHash()// As Byte() Implements IHasher.ComputeFileHash

        {

            byte[] ourHash;// As Byte



            //If file exists, create a HashAlgorithm instance based off of MD5 encryption
        //You could use a variant of SHA or RIPEMD160 if you like with larger hash bit sizes.






            HashAlgorithm ourHashAlg = MD5.Create();

            using (MemoryStream fileToHash = new MemoryStream(ByteArray))

            {

                //     'Compute the hash to return using the Stream we created.

                ourHash = ourHashAlg.ComputeHash(fileToHash);


          }





        return System.Convert.ToBase64String(ourHash);

    }
        public string Data { get; set; }
    }
}
