using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MobileDataKit.Core.Model;

namespace MobileDataKit_Collect.EntryControls
{
  public  class ControlFactory
    {

        private Field field = null;
        
        public BaseControl Create()
        {
           


            try
            {
                if (field.ControlType == "Date Control")
                    return new EntryControls.DateControlView(field, this.Section);
            }
            catch
            {

            }

            
            try
            {
                if ((from g in field.FieldOptions where g.Name.Length > 0 select g).Count() > 0)
                    return new SingleSelectView(field, this.Section);
            }
            catch(Exception ex)
            {

            }
            if (field.ControlType == "multi select")
                // return new SectionLabel(field);
                return new MultipleSelectFieldList(field, this.Section);
            return new TextBoxView(field, this.Section);

        }

        private ISection Section;
        public ControlFactory(Field _field, ISection section)
        {
            this.field = _field;
            this.Section = section;
           


        }
    }
}
