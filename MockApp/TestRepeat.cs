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
    public partial class TestRepeat : System.Windows.Forms.Form
    {
        public TestRepeat()
        {
            InitializeComponent();
            try
            {
                DependencyService.Cache.Add(typeof(MobileDataKit.Core.Model.Flow.IParser), typeof(TinyPG.Parser));
            }
            catch
            {

            }
           
        }


        public MobileDataKit.Core.Model.Form CreateNewEntry()
        {

            EntryTransaction.DefaultTransaction().Commit();



            var sssss = App.realm.All<MobileDataKit.Core.Model.Form>().First();

            MobileDataKit.Core.Model.Form.SetCurrentSection(sssss);
            EntryForm.CurrentEntryForm = App.realm.All<EntryForm>().Where(r => r.FormID !=null).First();
         



            return sssss;

        }

        private void TestRepeat_Load(object sender, EventArgs e)
        {
            var r = new Realms.RealmConfiguration("asasm");
            r.SchemaVersion = 2;
            App._realm = Realms.Realm.GetInstance(r);

            


            var form = CreateNewEntry();


            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            Realms.Realm realm = EntryTransaction.DefaultTransaction().Realm;


            form.CurrentField = string.Empty;
            var fwsws = (from v in form.Fields[0].Fields where v.Name == "45c8cafd-ef1a-4ee1-8b92-5b6e5fdf21d0" select v).First();
            var cont = new EntryControls.SectionLabel(fwsws);
            this.panel2.Controls.Add(cont);
            cont.Dock = DockStyle.Fill;
            Form1.currentControl = cont;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.currentControl.MoveNext();
        }
    }
}
