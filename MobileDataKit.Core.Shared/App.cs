using System;
using System.Collections.Generic;
using System.Text;
#if SERVER
namespace MobileDataKit.Core
{
public    class App
    {
        public static Realms.Realm _realm;
        public static Realms.Realm realm
        {
            get
            {
                if (_realm == null)
                    _realm =  Realms.Realm.GetInstance();
                return _realm;
            }
        }
    }

public class DependencyService
{
    public static T Get<T>()
    {
       
                return default(T);
    }
}
}
#endif


