using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Visual
{
    public class SingleSelect : BaseElement, ICollector
    {
        public List<BaseElement> Elements { get; set; } = new List<BaseElement>();

        public override Model.Field GetModel()
        {
            var f = new Model.Field();
            f.ControlType = type;
            f.Text = Label;
            f.Required = true;

            foreach(var d in this.Elements)
            {
                var fx = new Core.Model.FieldOption();
                fx.ID = Guid.NewGuid().ToString();
                try
                {
                    fx.Value = ((SingleSelectItem)d).value.ToString();
                }
                catch
                {
                    fx.Value = ((SingleSelectItem)d).Ref.ToString();

                }
               
                fx.Name = d.Label;
                f.FieldOptions.Add(fx);
            }
            return f;
        }

    }
}
