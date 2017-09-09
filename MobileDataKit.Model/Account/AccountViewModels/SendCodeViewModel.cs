using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDataKit.Model.Account.AccountViewModels
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }

       // public ICollection<SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
