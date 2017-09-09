#if MobileDataKit_Collect
using MobileDataKit_Collect;
#endif
using System;
using System.Collections.Generic;
using System.Text;
#if SERVER
using System.IO;
#endif
namespace MobileDataKit.Core.Model
{
    public class EntryForm : Realms.RealmObject
    {
     public   string CurrentField { get; set; }
#if SERVER
        public EntryForm()
        {
            this.EntryVariables = new List<EntryVariable>();
        }


        public void SaveJson()
        {
            System.IO.File.WriteAllText(System.IO.Path.Combine("Data", ID), Newtonsoft.Json.JsonConvert.SerializeObject(this));
        }

#endif


        public EntryForm GetParentForm()
        {
            if (string.IsNullOrWhiteSpace(FieldID))
                return null;

            var variable = App.realm.Find<EntryVariable>(FieldID);
            if (variable.EntryForm != null)
                return variable.EntryForm;


            return null;
        }
        [Newtonsoft.Json.JsonIgnore]
        public Form Form { get; set; }

        public EntryForm GetAdjacentAboveForm()
        {

            try
            {
                return _CurrentEntryForms[_CurrentEntryForms.IndexOf(this) - 1];
            }
            catch
            {

            }
            return null;
        }
        public static void RemoveCurrentForm()
        {
            if(CurrentEntryForm !=null)
            _CurrentEntryForms.RemoveAt(_CurrentEntryForms.Count - 1);
        }
        public static List<EntryForm> EntrySessionForms
        {
            get
            {
                if (_CurrentEntryForms == null)
                    _CurrentEntryForms = new List<EntryForm>();
                return _CurrentEntryForms;
            }
        }
        public int FormIndexNo { get; set; }
        public static EntryForm CurrentEntryForm
        {
            get
            {
                if (_CurrentEntryForms == null)
                    _CurrentEntryForms = new List<EntryForm>();
                if (_CurrentEntryForms.Count == 0)
                    return null;
                return _CurrentEntryForms[_CurrentEntryForms.Count - 1];
            }
            set
            {
                if (_CurrentEntryForms == null)
                    _CurrentEntryForms = new List<EntryForm>();

                _CurrentEntryForms.Add(value);
            }
        }


        private static List<EntryForm> _CurrentEntryForms;
        public IList<EntryVariable> EntryVariables { get; }

        [Realms.PrimaryKey]
        public string ID { get; set; }

        public string FieldID { get; set; }
        public string EntryStatus { get; set; }

        public string FormID { get; set; }

        public DateTimeOffset Date { get; set; }

        public string UserName { get; set; }

        public string CurrentVariable { get; set; }


        public IList<EntryForm> EntryForms { get; }


        public static EntryForm CreateNewEntry(ISection section)
        {

           
           

          
           
            var entryform = new EntryForm();
            
            if(typeof(Form) == section.GetType())
            {
                entryform.Form = (Form)section;

                entryform.FormID = section.Name;

            }


            if (typeof(Field) == section.GetType())
            {
               

                entryform.FieldID = section.Name;

            }
            entryform.ID = Guid.NewGuid().ToString();
            entryform.Date = DateTimeOffset.UtcNow;
            entryform.EntryStatus = "Incomplete";
              


            return entryform;

        }
    }
}
