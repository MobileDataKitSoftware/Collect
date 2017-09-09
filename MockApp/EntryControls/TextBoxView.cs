using MobileDataKit.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockApp.EntryControls
{
public    class TextBoxView : BaseControl
    {

        public override void MoveNext()
        {
            this.entryVariable.Value = textbox.Text.Trim();
            base.MoveNext();
        }
        private System.Windows.Forms.TextBox textbox = null;
        public TextBoxView(Field field, ISection section) : base(field, section)
        {

            var panel = new System.Windows.Forms.FlowLayoutPanel();
            panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            var label = new System.Windows.Forms.Label();
            label.Text = field.Text;
            panel.Controls.Add(label);

             textbox = new System.Windows.Forms.TextBox();
        //    label.Text = field.Text;
            panel.Controls.Add(textbox);

            this.Controls.Add(panel);




        }

    }
}
