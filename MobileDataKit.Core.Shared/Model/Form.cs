using Newtonsoft.Json;
#if SERVER

using OfficeOpenXml;
#endif
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if SERVER
#else
using Xamarin.Forms;
#endif
namespace MobileDataKit.Core.Model
{

    
    public  class Form :Realms.RealmObject, ISection
    {



       
        public static string SessionID;

        public string Expression { get; set; }
        public string CurrentField { get; set; }
        private MobileDataKit_Collect.Model.FlowManager flowmanager = new MobileDataKit_Collect.Model.FlowManager();

        public Core.Model.Field GetNextField(string sectionid, Field current = null)
        {


            return flowmanager.GetNextField(this, sectionid, current);
        }
        public override string ToString()
        {
            return this.Name + "-" + this.Text;
        }
        [Realms.PrimaryKey]
  public string ID { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Project Project { get; set; }
        public double Version { get; set; }
        public static ISection form
        {
            get
            {
                return sections[sections.Count - 1];
            }
        }
        public void SetChilds()
        {
            foreach (var ls in Fields)
            {
                ls.Form = this;
                ls.SetChilds();
            }
        }
        

    public static void SetCurrentSection(ISection Section)
        {
            if (sections == null)
                sections = new List<ISection>();

            sections.Add(Section);
        }

        private static List<ISection> sections = new List<ISection>();

        public string Status { get; set; } 
        public string VersionStatus { get; set; }
        
        public string Text { get; set; }
       


        public string ProjectID { get; set; }
        public string Name { get; set; }

        public void UpdateValues(Form f)
        {
            this.Name = f.Name;
            //this.ID = Guid.NewGuid();
        }
        public IList<Field> Fields { get; }
        public IList<Model.EntryForm> EntryForms { get; }
        public IList<Model.EndPoints.RemoteFormEndPoint> EndPoints { get; }

#if SERVER
        public Form()
        {
        this.Name =Guid.NewGuid().ToString();
            Fields = new List<Field>();
        this.EndPoints =new List<EndPoints.RemoteFormEndPoint>();
        }

        public static Form ParseExcelCustom(Byte[] file)
        {
            Form f = new Form();
            f.ID = Guid.NewGuid().ToString();
            using (var mem = new System.IO.MemoryStream(file))
            {

       
                using (ExcelPackage package = new ExcelPackage(mem))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    bool bHeaderRow = true;
                    var collection = f.Fields;
                    f.Name = worksheet.Cells[1, 2].Value.ToString();

                    var sections = new List<Field>();
                    for (int row = 2; row <= rowCount; row++)
                    {
                        string fieldtype = string.Empty;
                        if (worksheet.Cells[row, 3].Value != null)
                            fieldtype = worksheet.Cells[row, 3].Value.ToString();


                        if (fieldtype.ToLower() == "end section" || fieldtype.ToString() == "end multi select")
                        {
                            if (sections.Count > 0)
                                sections.RemoveAt(sections.Count - 1);


                        }







                        if (fieldtype.ToLower() == "section" || fieldtype.ToLower() == "multi select")
                        {
                            var section = new Field();
                            section.No = f.Fields.Count + 1;
                            if(worksheet.Cells[row, 2].Value !=null)
                            section.Text = worksheet.Cells[row, 2].Value.ToString();


                            section.ControlType = fieldtype;
                            if (sections.Count == 0)
                                f.Fields.Add(section);
                            else
                                sections[sections.Count - 1].Fields.Add(section);




                            if (worksheet.Cells[row, 5].Value != null)
                                section.Precondition = worksheet.Cells[row, 5].Value.ToString();

                            if (worksheet.Cells[row, 6].Value != null)
                                section.PostCondition = worksheet.Cells[row, 6].Value.ToString();


                            if (worksheet.Cells[row, 7].Value != null)
                                section.Expression = worksheet.Cells[row, 7].Value.ToString();



                            sections.Add(section);
                        }



                        var variabletext = string.Empty;

                        if (worksheet.Cells[row, 2].Value != null)
                            variabletext = worksheet.Cells[row, 2].Value.ToString();

                        var variablename = string.Empty;

                        if (worksheet.Cells[row, 1].Value != null)
                            variablename = worksheet.Cells[row, 1].Value.ToString();




                        if (!String.IsNullOrWhiteSpace(variabletext) && fieldtype.ToLower() != "section" && fieldtype.ToLower() != "multi select" )
                            {
                            var field = new Field();
                            field.Text = variabletext;

                            field.Name = variablename;




                            if (worksheet.Cells[row, 5].Value != null)
                                field.Precondition = worksheet.Cells[row, 5].Value.ToString();

                            if (worksheet.Cells[row, 6].Value != null)
                                field.PostCondition = worksheet.Cells[row, 6].Value.ToString();


                            if (worksheet.Cells[row, 7].Value != null)
                                field.Expression = worksheet.Cells[row, 7].Value.ToString();

                            if (fieldtype == "Date Control" || fieldtype == "Numeric")
                                field.ControlType = fieldtype;

                            if (fieldtype == "Text")
                                field.ControlType = "Single Line Text";

                            if (worksheet.Cells[row, 4].Value != null)
                            {
                                var ggg = worksheet.Cells[row, 4].Value.ToString().Trim().Split(";".ToCharArray());
                                var cc = 1;
                                foreach (var vb in ggg)
                                {
                                    if(!String.IsNullOrWhiteSpace(vb.Trim()))
                                        {
                                        var opt = new FieldOption();
                                        opt.Name = vb;
                                        opt.Value = cc.ToString();
                                        field.FieldOptions.Add(opt);
                       }


                                    cc = cc + 1;
                                }
                            }













                            if (sections.Count > 0)
                                sections[sections.Count - 1].Fields.Add(field);
                            else
                                f.Fields.Add(field);
              
}
                    }
                }


            }


            return f;
        }


                public static Form ParseExcel(Byte[] file)
        {

            Form f = new Form();

            //  Getting Single Select items
           
            using (var mem = new System.IO.MemoryStream(file))
            {


                using (ExcelPackage package = new ExcelPackage(mem))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    bool bHeaderRow = true;
                    var collection = f.Fields;
                  f.Name =  worksheet.Cells[0, 1].Value.ToString();
                    for (int row = 1; row <= rowCount; row++)
                    {
                        Field input = null;


                        if (worksheet.Cells[row, 1].Value != null)
                        {

                            var _type = string.Empty;
                            var name = string.Empty;
                            var text = string.Empty;
                            var options = string.Empty;
                            if (worksheet.Cells[row, 3].Value !=null)
                  _type     = worksheet.Cells[row, 3].Value.ToString();

                            if (worksheet.Cells[row, 1].Value != null)
                                name = worksheet.Cells[row, 1].Value.ToString();

                            if (worksheet.Cells[row, 2].Value != null)
                                text = worksheet.Cells[row, 2].Value.ToString();

                            if (worksheet.Cells[row, 4].Value != null)
                                options = worksheet.Cells[row, 4].Value.ToString();

                                if (_type =="begin group")
                            {
                                input = new Field();
                                collection.Add(input);
                                collection = input.Fields;

                                
                            }
                            else if(_type == "end group")
                            {
                                collection = f.Fields;
                            }
                            else
                            {
                                input = new Field();
                                input.Name = name;
                                input.Text = text;

                                
                                    var optvalue = 0;
                                    foreach(var t in options.Split(";".ToCharArray()))
                                    {
                                        var field_opt = new FieldOption();
                                        field_opt.Value = optvalue.ToString();
                                        field_opt.Name = t;
                                        input.FieldOptions.Add(field_opt);
                                        optvalue = optvalue + 1;
                                    }

                                


                            }
                            
                        }
                    }

                }



            }



            return f;

        }
#else
        
#endif

            //var namesgenerator = new InternalNameGenerator();
            //namesgenerator.Generate(ID);




        }
}
