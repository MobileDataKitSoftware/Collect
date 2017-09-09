using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MobileDataKit_Collect
{
	public class QuestioneerPage : ContentPage
	{

        private Realms.Realm realm;
        public QuestioneerPage ()
        {
            realm = Realms.Realm.GetInstance();
            var sssss = realm.All<Form>().ToList()[1];

           // Content = new SectionView(realm, sssss.Sections[0]);
		}
	}
}
