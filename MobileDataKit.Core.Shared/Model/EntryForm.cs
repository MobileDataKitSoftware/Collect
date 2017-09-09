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
     public string GetCurrentField(string sectionID)
        {
            if (this.CurrentFieldTracks.Count == 0)
                return string.Empty;

            var t = this.CurrentFieldTracks.Count;
            while (t > 0)
            {
                var variable_name = this.CurrentFieldTracks[t-1];
                if ( variable_name.SectionID == sectionID)

                {
                    return variable_name.FieldName;
                }

                t = t - 1;
            }

            return string.Empty;
        }

        public void SetCurrentField(string field_name, string sectioID)
        {
            var t2 = this.CurrentFieldTracks.Count;
            while (t2 > 0)
            {
                var variable_name = this.CurrentFieldTracks[t2 - 1];
                if (variable_name.SectionID == sectioID && variable_name.FieldName ==field_name)

                {
                    return;
                  
                }
                if (variable_name.SectionID == sectioID)
                    break;


                t2 = t2 - 1;
            }

            var t = new CurrentFieldTrack();
            t.EntryForm = this;
            t.SectionID = sectioID;
            t.No = this.CurrentFieldTracks.Count + 1;
            t.FieldName = field_name;
            t.ID = Guid.NewGuid().ToString();
            this.CurrentFieldTracks.Add(t);
        }

        public void RemoveCurrentField(string field_name, string sectionID)
        {
            if (this.CurrentFieldTracks.Count == 0)
                return;
            var t = this.CurrentFieldTracks.Count - 1;
            while(t>0)
            {
                var variable_name = this.CurrentFieldTracks[t];
                if(variable_name.FieldName ==field_name  && variable_name.SectionID ==sectionID)

                {
                    this.CurrentFieldTracks.Remove(variable_name);
                    break;
                }

                t = t - 1;
            }
            
            

        }
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


        public IList<CurrentFieldTrack> CurrentFieldTracks { get; }

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
