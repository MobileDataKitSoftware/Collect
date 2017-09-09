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
using RestSharp;
using System.Text.RegularExpressions;

[assembly: Xamarin.Forms.Dependency(typeof(MobileDataKit_Collect.Droid.Login))]
namespace MobileDataKit_Collect.Droid
{
    public class Login : Auth.ILogin
    {
        void ILogin.Login(string username, string password)
        {
            var client = new RestClient(Model.AppConstants.Url);// 'Web.HttpContext.Current.Request.Url.OriginalString.Replace(Web.HttpContext.Current.Request.Url.PathAndQuery, String.Empty) & " / ")

            var request = new RestRequest("connect/token", Method.POST);

            request.AddParameter("Content-Type", "application/x-www-form-urlencoded", ParameterType.HttpHeader);
            //request.AddHeader("CompanyCode", "Promoter");
            request.AddParameter("grant_type", "password");
            request.AddHeader("Accept", "application/json");

            //  request.AddJsonBody(log);
            request.AddParameter("UserName", username);
            request.AddParameter("Password", password);

            //var request = new RestSharp.RestRequest("connect/token", RestSharp.Method.POST);

            //request.AddParameter("Content-Type", "application/x-www-form-urlencoded",RestSharp.ParameterType.HttpHeader);
            //request.AddHeader("CompanyCode", "Promoter");
            //request.AddParameter("grant_type", "password");
            //request.AddHeader("Accept", "application/json");
            //request.AddParameter("username", this.usernameEntry.Text);
            //request.AddParameter("password", this.passwordEntry.Text);
          
            var response = client.Execute<Auth.LoginResult>(request);
        }
    }
}