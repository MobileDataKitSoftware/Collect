using MobileDataKit.Core.Model;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Realtime
{
  public  class RealtimeVariableSocket
    {
        protected static RealtimeVariableSocket instance;
        private  Websockets.IWebSocketConnection _connection;

        public static async Task<RealtimeVariableSocket> InstanceAsync()
        {
            if (instance == null)
            {
                if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected && await Plugin.Connectivity.CrossConnectivity.Current.IsRemoteReachable(Model.AppConstants.Host, Model.AppConstants.Port))
                {
                    instance = new RealtimeVariableSocket();
                    instance.Connect();
                }
                else
                    instance = new FakeRealtimeVariableSocket();

            }

            return instance;
        }

        protected virtual void Connect()
        {
            _connection = Websockets.WebSocketFactory.Create();
            _connection.OnOpened += Connection_OnOpened;
            _connection.OnMessage += Connection_OnMessage;
            _connection.OnClosed += Connection_OnClosed;
            _connection.OnError += Connection_OnError;
            _connection.Open(Model.AppConstants.Url + "formvariablesocket");
        }

        protected RealtimeVariableSocket()
        {
            IsBusy = false;
           
           


        }

        private List<EntryVariable> Load = new List<EntryVariable>();
        public bool IsBusy
        {
            get; private set;
        }


        public virtual async Task UploadDataAsync()
        {
         
            if (_failed)
            {
                instance = null;
                return;
            }
            if (IsBusy)
                return;
         
          

            var fff =App.realm.All<EntryVariable>().Where(a => a.IsDirty ==true).ToList();
            foreach (var variable in fff)
            {

                Load.Add(variable);

            }

            if(Load.Count>0)
                IsBusy = true;

            foreach (var variable in fff)
            {
                var cards = new List<MobileDataKit.Core.Model.EndPoints.RemoteFormEndPoint>();
             foreach(var t in   variable.EntryForm.Form.EndPoints)
                {
                    cards.Add(t);
                }

                var ecr = Xamarin.Forms.DependencyService.Get<Encryption.IEncryptionFactory>();
               var data_to_Send= ecr.Encrypt(new { FieldID = variable.FieldID, EntryFormID = variable.EntryFormID, Value = variable.Value, ID = variable.ID }, cards);
                _connection.Send(data_to_Send);


            }

        }

        private static void Connection_OnClosed()
        {

            //   Debug.WriteLine("Closed !");
        }
        bool _failed = false;
        private void Connection_OnError(string obj)
        {
            _failed = true;
            System.Diagnostics.Debug.WriteLine("ERROR " + obj);
        }

        private void Connection_OnOpened()
        {
            System.Diagnostics.Debug.WriteLine("Opened !");
        }

        private void Connection_OnMessage(string obj)
        {

            var inMemoryObject = (from v in Load where v.ID == obj select v).First();

            var o = App.realm.All<EntryVariable>().Where(a => a.ID == obj).First();
          
            if(inMemoryObject.Value ==o.Value)
            using (var t = App.realm.BeginWrite())
            {
                o.IsDirty=false;
                t.Commit();
            }

            Load.Remove(inMemoryObject);

            if (Load.Count == 0)
                IsBusy = false;


        }
    }
}
