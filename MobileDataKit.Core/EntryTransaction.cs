

using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Core.Model
{
 public   class EntryTransaction : IDisposable
    {

        public EntryTransaction()
        {
            Realm = App.realm;
            this.Transaction = this.Realm.BeginWrite();
        }


        public void CommitAndStartTransaction()
        {
            
                if(this.Realm.IsInTransaction)
                this.Transaction.Commit();
           
         
            this.Transaction = this.Realm.BeginWrite();
        }

        public void Commit()
        {
            try
            {

                this.Transaction.Commit();
            }
            catch
            {

            }

            this.Transaction = null;

        }
        private static EntryTransaction _default = null;
        public Realms.Realm Realm { get; }
        public Realms.Transaction Transaction { get; private set; }
        public static EntryTransaction DefaultTransaction()
        {
            if (_default == null)
            {
                _default = new EntryTransaction();
                

            }

            if (_default.Transaction == null)
                _default.Transaction = _default.Realm.BeginWrite();

            return _default;
        }

        public void Dispose()
        {
            CommitAndStartTransaction();
        }
    }
}
