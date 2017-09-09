using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace MobileDataKit_Collect.EntryControls
{
	public class MultipleSelectViewBase : EntryControls.BaseControl
    {

        public override void SetNull()
        {


            foreach (var dx in this.Field.Fields)
            {
                
            

                EntryVariable child_variable = null;
                var dd = dx.Name;

                if (EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == dd).Count() > 0)
                    child_variable = EntryForm.CurrentEntryForm.EntryVariables.Where(d => d.FieldID == dd).First();

                if (child_variable != null)
                    child_variable.Value = null;
                
            }

        }
       

      public MultipleSelectViewBase(Field field, ISection section) : base( field, section)
        {

        }

       
    }

    public class SwitchValueCpnverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            if ((int)value == 1)
                return true;

            return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if ((bool)value == false)
                return null;

            if ((bool)value)
                return 1;
            return null;
         
        }
    }






    public class PickerValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;



       
            var cd = (IList<FieldOption>)parameter;
           


            try
            {
                foreach(var c in cd)
                {
                    if (c.Value == value.ToString())
                        return c.Name;
                }
                //return cd[int.Parse(value.ToString().Trim()) - 1];
            }
            catch (Exception dc)
            {
                var ds = dc;

            }


            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return null;
            if (string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            var cd = (IList<FieldOption>)parameter;
            var d = 0;
            foreach(var c in cd)
            {
                if (d.ToString() == value.ToString())
                    return c.Value;
                d = d + 1;
            }
            //   var fffffffffffffffffffffffffff= ddd[value];

            return null;

        }
    }
}
