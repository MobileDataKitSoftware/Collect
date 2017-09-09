using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Rest
{
  public  class MdkRestRequest : RestRequest
    {

        public MdkRestRequest(string url, Method method):base(url,method)
        {
            // AddHeader("Content-Type", "application/x-www-form-urlencoded");
           // = RestSharp.DataFormat.Json;
          //  this.Timeout = 1000 * 60 * 5;
            //AddParameter("Authorization", "Bearer " + SafeUrl.Token, RestSharp.ParameterType.HttpHeader);
            //AddParameter("phoneno", SafeUrl.UserName, RestSharp.ParameterType.HttpHeader);

            //AddParameter("Pass", SafeUrl.Token, RestSharp.ParameterType.HttpHeader);
            //if (method != RestSharp.Method.GET)
            //    AddHeader("Accept", "application/json");
        }
    }
}
