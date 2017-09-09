using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileDataKit_Collect
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormList : ContentPage
    {
        public FormList()
        {
            InitializeComponent();
           // BindingContext = new FormListViewModel();
        }

        private async Task formlist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await this.Navigation.PushAsync(new ProjectMainPage((ProjectInfoFormsInfo)e.Item));
        }
    }

 
}