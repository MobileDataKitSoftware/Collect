using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit_Collect.Model
{
   public class MultipleSelectFieldListItem: System.ComponentModel.INotifyPropertyChanged
    {

        public string Text { get; set; }
        
        public MultipleSelectFieldListItem(MobileDataKit.Core.Model.EntryVariable entryVariable, string text)
        {
            this.Text = text;
            this.EntryVariable = entryVariable;
        }
        
        private MobileDataKit.Core.Model.EntryVariable EntryVariable = null;

        public event PropertyChangedEventHandler PropertyChanged;


        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Value
        {
            get
            {
                if (this.EntryVariable.Value == "1")
                    return true;
                return false;

            }
            set
            {
                if (value)
                
                    this.EntryVariable.Value = "1";
                else
                    this.EntryVariable.Value = "0";
                MobileDataKit.Core.Model.EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
                OnPropertyChanged();
                OnPropertyChanged("Value");

            }
        }
    }
}
