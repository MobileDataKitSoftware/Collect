using Flurl;
using Flurl.Http;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDataKit_Collect
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

#if DEBUG
            this.usernameEntry.Text = "ayubumasasi@gmail.com";
            this.passwordEntry.Text = "123456a*";
#endif
        }

        private Auth.IUserStore UserStore { get; set; }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.SignUpPage());
        }
        async Task ShowHomePage()
        {
            //  PasswordDeriveBytes s = null;
            var j = DependencyService.Get<Encryption.IEncryptionKeyGenerator>();
            if (string.IsNullOrWhiteSpace(UserStore.UserReam))
                UserStore.RefreshValue(this.usernameEntry.Text.ToLower(), this.passwordEntry.Text);
            var config = new Realms.RealmConfiguration(UserStore.UserReam +".realm");
            config.SchemaVersion = 3;
                      config.EncryptionKey = j.GetKey(this.passwordEntry.Text, this.usernameEntry.Text.ToLower());


        
      
            try
            {
                App.realm = Realms.Realm.GetInstance(config);
            }
            catch(Exception ex)
            {
                var r = ex;
            }

            
            App.IsUserLoggedIn = true;

            var client = new RestClient(Model.AppConstants.Url);// 'Web.HttpContext.Current.Request.Url.OriginalString.Replace(Web.HttpContext.Current.Request.Url.PathAndQuery, String.Empty) & " / ")

            var request = new RestRequest("api/Device", Method.POST);

            //request.AddParameter("Content-Type", "application/x-www-form-urlencoded",RestSharp.ParameterType.HttpHeader);
            //request.AddHeader("CompanyCode", "Promoter");
            //request.AddParameter("grant_type", "password");
            request.AddHeader("Accept", "application/json");
            
           
 App.IsUserLoggedIn = true;
          
            var gg = new ProjectInfoFormsInfo();
            var fggggggggggggg = App.realm.All<MobileDataKit.Core.Model.Form>().Where(f => f.ID == "f72882ff-c889-4e77-9aba-fea3d7d26bee").First();
            gg.ID = fggggggggggggg.ID;
            gg.Name = fggggggggggggg.Name;
            Navigation.InsertPageBefore(new ProjectMainPage(gg), this);
            await Navigation.PopAsync();

        }
            async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            UserStore = DependencyService.Get<Auth.IUserStore>();

            UserStore.RefreshValue(this.usernameEntry.Text, this.passwordEntry.Text);

            if(!string.IsNullOrWhiteSpace(UserStore.PasswordHash))
            if(Auth.PasswordHash.ValidatePassword(this.passwordEntry.Text, UserStore.PasswordHash))
            {



                   
                    await ShowHomePage();
                    return;

            }

            Auth.LoginResult response = null;
            using (var client2 = new HttpClient())
            {
                client2.BaseAddress = new Uri(Model.AppConstants.Url);
                var prms = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", this.usernameEntry.Text),
                        new KeyValuePair<string, string>("password", this.passwordEntry.Text),

                    };

                var response2 = client2.PostAsync("connect/token", new FormUrlEncodedContent(prms)).Result;
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<Auth.LoginResult>( response2.Content.ReadAsStringAsync().Result);
                var log = new Model.Device();
                //  log.UserName = this.usernameEntry.Text;
                log.CreatedBy = usernameEntry.Text;
                log.IMEI = Plugin.DeviceInfo.CrossDevice.Hardware.DeviceId;


                //    var sdsds= await client.Execute(request);

                try
                {
                    var sdsds = await Model.AppConstants.Url.AppendPathSegments("api", "Device").PostJsonAsync(log);
                }
                catch(Exception ex)
                {
                    var erere = ex;
                }
               
            }


            if (!string.IsNullOrWhiteSpace(response.access_token))
            {
                Auth.Manager.CredentialManager.SaveCredentials(this.usernameEntry.Text, Auth.PasswordHash.CreateHash(this.passwordEntry.Text), response.access_token);
                await ShowHomePage();
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }

       
    }
}