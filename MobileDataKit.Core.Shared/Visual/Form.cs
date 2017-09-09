#if SERVER
using OfficeOpenXml;
#endif
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileDataKit.Core.Visual
{
    public class Form : ICollector
    {


        public  Core.Model.Form GetModel()
        {
            var form = new Core.Model.Form();
            
            foreach (var f in this.Elements)
            {
                form.Fields.Add(f.GetModel());
            }
            return form;
        }
        public List<BaseElement> Elements { get; set; } = new List<BaseElement>();

        public static List<System.Xml.Linq.XElement> GetElements(System.Xml.Linq.XElement element)
        {

            var x = new List<System.Xml.Linq.XElement>();
            foreach (var d in element.Descendants())
            {

                if (d.Parent.Equals(element))
                    x.Add(d);
            }
            return x;
        }
        public static System.Xml.Linq.XElement GetBindElement( string path, IEnumerable<System.Xml.Linq.XElement> bind_elements)
        {
            path = "/data/" + path;
      
            foreach (var d in bind_elements)
            {
                if (d.Name.LocalName == "bind")

                {
                    

                    var sdsdsdsd = d.Attribute("nodeset");
                    var a = sdsdsdsd;
                    if (d.Attribute("nodeset").Value == path)
                        return d;
                }
                
            }
            return null;   
        }
        public static System.Xml.Linq.XElement GetElement(System.Xml.Linq.XElement element,string name)
        {

            var x = new List<System.Xml.Linq.XElement>();
            foreach (var d in element.Descendants())
            {

                if (d.Parent.Equals(element) && d.Name.LocalName == name)
                    return d;
            }
            return null;
        }

       
        public static ICollector Parse(ICollector element_object, System.Xml.Linq.XElement element, IEnumerable<System.Xml.Linq.XElement> bind_elements)
        {
            var child_elements = GetElements(element);
            foreach (var ele in child_elements)
            {
                BaseElement ele_obj = null;
                if (ele.Name.LocalName == "group")
                {
                    ele_obj = new Group();
                    Parse((Group)ele_obj, ele, bind_elements);
                    element_object.Elements.Add(ele_obj);
                }
                if(ele.Name.LocalName == "label")
                {
                    ele_obj = new Label();
                    ((Label)ele_obj).Text = ele.Value;
                    element_object.Elements.Add(ele_obj);
                }

                if (ele.Name.LocalName == "input")
                {

                    ele_obj = new Input();
                    element_object.Elements.Add(ele_obj);
                }
                if (ele.Name.LocalName == "select1")
                {
                    ele_obj = new SingleSelect();
                    element_object.Elements.Add(ele_obj);
                    foreach(var item in GetElements(ele))
                    {
                        if(item.Name.LocalName == "item")
                        {
                            var singleitem = new SingleSelectItem();
                            var label_element1 = GetElement(ele, "label");
                            if (label_element1 != null)
                                singleitem.Label = label_element1.Value;

                            label_element1 = GetElement(ele, "value");
                            if (label_element1 != null)
                                singleitem.value = label_element1.Value;

                            ((ICollector)ele_obj).Elements.Add(singleitem);
                        }
                    }
                }
                if(ele.Attribute("ref") !=null)
                ele_obj.Ref = ele.Attribute("ref").Value;

                var label_element = GetElement(ele, "label");
                if(label_element !=null)
                ele_obj.Label = label_element.Value;
                var ed = ele_obj.VariableName;
                if(!string.IsNullOrWhiteSpace(ele_obj.Ref))
                {
                    var cd = GetBindElement(ed,bind_elements);
                    if(cd !=null)
                    {
                        var required_at = cd.Attribute("required");
                        if (required_at != null)
                            ele_obj.required = required_at.Value;
                        var type_at = cd.Attribute("type");
                        if (type_at != null)
                            ele_obj.type = type_at.Value;

                        var nodeset_at = cd.Attribute("nodeset");
                        if (nodeset_at != null)
                            ele_obj.nodeset = nodeset_at.Value;

                        var relevant_at = cd.Attribute("relevant");
                        if (relevant_at != null)
                            ele_obj.relevant = relevant_at.Value;
                    }
                }
                
                
                
            }

            return element_object;
        }
        #if SERVER
        public static Form ParseExcel(Byte[] file)
        {

            Form f = new Form();

            //  Getting Single Select items
            var items = new Dictionary<string, List<SingleSelectItem>>();
            using (var mem = new System.IO.MemoryStream(file))
            {


                using (ExcelPackage package = new ExcelPackage(mem))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[2];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var list_name = worksheet.Cells[row, 1].Value.ToString();
                        var item = new SingleSelectItem();
                        item.Label = worksheet.Cells[row, 3].Value.ToString();
                        item.value = worksheet.Cells[row, 2].Value.ToString();

                        if (!items.ContainsKey(list_name))
                            items.Add(list_name, new List<SingleSelectItem>());

                        items[list_name].Add(item);
                    }


                }
            }
                    using (var mem = new System.IO.MemoryStream(file))
            {


                using (ExcelPackage package = new ExcelPackage(mem))
                {
                   
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    bool bHeaderRow = true;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        BaseElement input = null;
                        if (worksheet.Cells[row, 1].Value != null)
                        {

                            var _type = worksheet.Cells[row, 1].Value.ToString();
                            if (_type == "text" || _type == "integer")
                                input = new Input();
                            if (_type.Contains("select_one"))
                            {
                                input = new SingleSelect();
                                var ffff = (SingleSelect)input;
                                ffff.Elements.AddRange(items[worksheet.Cells[row, 2].Value.ToString()]);
                            }


                            if (input != null)
                            {
                                if (worksheet.Cells[row, 3].Value != null)
                                    input.Label = worksheet.Cells[row, 3].Value.ToString();
                                if (worksheet.Cells[row, 2].Value != null)
                                    input.Ref = worksheet.Cells[row, 2].Value.ToString();

                                input.type = _type;

                                f.Elements.Add(input);
                            }
                        }
                    }
                 
                }



            }



            return f;

        }

        
#endif
            public static Form ParseXml(string file)
        {
         var form_view=   new Form();
         
            var xml_doc = System.Xml.Linq.XDocument.Parse(file);
           
            var sddfd = xml_doc.Root.Descendants();
          //Get the body first
           System.Xml.Linq.XElement body_element = null;
            foreach (var d in sddfd)
            {

                if (d.Name.LocalName == "body")
                {
                    body_element = d;
                    break;
                }
            }
            return (Form)Parse(form_view, body_element, sddfd);
            

          
        }
    }
}
