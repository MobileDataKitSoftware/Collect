using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockApp.EntryControls
{
  public  class ControlFactory
    {


        private Field field = null;

        public BaseControl Create()
        {



            //try
            //{
            //    if (field.ControlType == "Date Control")
            //        return new EntryControls.DateControlView(field, this.Section);
            //}
            //catch
            //{

            //}

            //try
            //{
            //    if (field.Fields.Count > 0)
            //        return new MultipleSelectViewIndividualQn(field, this.Section);
            //}
            //catch (Exception ex)
            //{

            //}
            //try
            //{
            //    if ((from g in field.FieldOptions where g.Name.Length > 0 select g).Count() > 0)
            //        return new SingleSelectView(field, this.Section);
            //}
            //catch (Exception ex)
            //{

            //}

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
