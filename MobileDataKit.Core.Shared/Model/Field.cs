
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if SERVER
#else
using Xamarin.Forms;
using MobileDataKit_Collect;
#endif

namespace MobileDataKit.Core.Model
{

   
    
    public partial class Field : Realms.RealmObject, ISection
    {
        public void UpdateValues(Field f)
        {
            this.Text = f.Text;
            this.ControlType = f.ControlType;
            this.Name = f.Name;
            this.No = f.No;
            

        }

        public string CurrentField { get; set; }


        private MobileDataKit_Collect.Model.FlowManager _flowmanager = new MobileDataKit_Collect.Model.FlowManager();

        public Core.Model.Field GetNextField(string sectionid, Field current = null)
        {


            return _flowmanager.GetNextField(this, sectionid, current);
        }


        public EntryVariable CreateEntryVariable(object value=null)
        {
            _flowmanager.isection = this;
            return _flowmanager.CreateEntryVariable(value);
        }
        public void SetChilds()
        {
            foreach(var child in Fields)
            {
                child.ParentField = this;
                child.Form = null;
                child.FieldID = this.Name;
                child.SetChilds();
            }

            foreach (var opt in this.FieldOptions)
                opt.Field = this;
        }
        public string FormID { get; set; }
        public string SectionID { get; set; }
       

       public string Precondition { get; set; }


        public string PostCondition { get; set; }
      

        public string Expression { get; set; }
        public  bool ShowInDashBoard { get; set; }


        public string FieldID { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return Name + "- " + Text;
        }

        public string Text { get; set; }

        public string ControlType { get; set; }
        [Realms.PrimaryKey]
#if SERVER
        [System.ComponentModel.DataAnnotations.Key]
#endif
        public string Name { get; set; }
        public Boolean Required { get; set; }

        public int No { get; set; }

        public IList<Field> Fields { get; }

        
        [Newtonsoft.Json.JsonIgnore]
        public Field ParentField { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public Form Form { get; set; }
        public IList<FieldOption> FieldOptions { get; }

#if SERVER

        public Field()
        {
        this.Required =true;
            this.FieldOptions = new List<FieldOption>();
            this.Fields = new List<Field>();
         this.Name =Guid.NewGuid().ToString();
        }

#endif
    }
}
