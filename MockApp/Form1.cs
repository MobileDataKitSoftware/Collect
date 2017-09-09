using MobileDataKit.Core;
using MobileDataKit.Core.Model;
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
    public partial class Form1 : System.Windows.Forms.Form
    {
      
        public Form1()
        {
            InitializeComponent();

          DependencyService.Cache.Add(typeof(MobileDataKit.Core.Model.Flow.IParser), typeof(TinyPG.Parser));
        }
        public MobileDataKit.Core.Model.Form CreateNewEntry()
        {

            EntryTransaction.DefaultTransaction().Commit();

            

            var sssss = App.realm.All<MobileDataKit.Core.Model.Form>().First();

            MobileDataKit.Core.Model.Form.SetCurrentSection(sssss);
            using (var csssss = App.realm.BeginWrite())
            {
                EntryForm.CurrentEntryForm = EntryForm.CreateNewEntry(sssss);

                App.realm.Add(EntryForm.CurrentEntryForm);
                csssss.Commit();
            }



            return sssss;

        }

        public static BaseControl currentControl = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            var r = new Realms.RealmConfiguration("Form1.realm");
           App._realm = Realms.Realm.GetInstance(r);

            var sdssd = System.IO.File.ReadAllText("TextFile1.txt");
            var gg = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MobileDataKit.Core.Model.Project>>(sdssd);

            foreach (var pr in gg)
            {
                foreach (var fo in pr.Forms)
                {
                    fo.Project = pr;
                    foreach (var ls in fo.Fields)
                    {
                        ls.Form = fo;
                        ls.SetChilds();
                    }
                }
            }

            using (var tr =App.realm.BeginWrite())
            {
                foreach (var pr in gg)
                {
                   App.realm.Add(pr, true);


                }
                tr.Commit();
            }



            var form = CreateNewEntry();


            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            Realms.Realm realm = EntryTransaction.DefaultTransaction().Realm;


            form.CurrentField = string.Empty;
            var cont = new EntryControls.SectionLabel(form);
            this.panel2.Controls.Add(cont);
            cont.Dock = DockStyle.Fill;
            currentControl = cont;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentControl.MoveNext();
        }
    }
}
