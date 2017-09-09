using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MockApp
{
    public partial class ExcelParser : Form
    {
        public ExcelParser()
        {
            InitializeComponent();
        }

        private void ExcelParser_Load(object sender, EventArgs e)
        {
            var ff = MobileDataKit.Core.Model.Form.ParseExcelCustom(System.IO.File.ReadAllBytes(@"C:\Users\ayubu\Documents\Visual Studio 2017\Projects\ExcelToODK2\bin\Debug\Copy of Questineer (1).xlsx"));

        }
    }
}
