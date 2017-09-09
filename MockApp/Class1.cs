using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms
{
    class Class1
    {

        
    }
}
namespace MobileDataKit_Collect
{
    class sds
    {
    }

}


public class DependencyService
{

    public static Dictionary<Type, Type> Cache = new Dictionary<Type, Type>();

    public static T Get<T>()
    {
        var tt = typeof(T);

        if (Cache.ContainsKey(tt))
        {
            return (T)Activator.CreateInstance(Cache[tt]);
        }
        return default(T);
    }
}