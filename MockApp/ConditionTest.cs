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
    public partial class ConditionTest : System.Windows.Forms.Form
    {
        public ConditionTest()
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



            var sssss = Newtonsoft.Json.JsonConvert.DeserializeObject<MobileDataKit.Core.Model.Form>(Properties.Resources.HuruDef);// ().First();
            sssss.Fields[11].PostCondition = "if BirthDate<7 | BirthDate>25";
            MobileDataKit.Core.Model.Form.SetCurrentSection(sssss);
            using (var csssss = App.realm.BeginWrite())
            {
                EntryForm.CurrentEntryForm = EntryForm.CreateNewEntry(sssss);

                App.realm.Add(EntryForm.CurrentEntryForm,true);
                csssss.Commit();
            }



            return sssss;

        }

        private void TestRepeat_Load(object sender, EventArgs e)
        {
            var r = new Realms.RealmConfiguration("TestCondition");
            r.SchemaVersion = 2;
            App._realm = Realms.Realm.GetInstance(r);



            return;
            var form = CreateNewEntry();
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            var reg = form.Fields[0];

            var reg_value = reg.CreateEntryVariable(5);
            reg_value.Value = 5.ToString();

            var dis = form.Fields[1];
            var dis_value = dis.CreateEntryVariable(8);
            dis_value.Value = 8.ToString();
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();

            var parser = DependencyService.Get<MobileDataKit.Core.Model.Flow.IParser>();
            var res = parser.ParseExpression(dis.Precondition).Eval(EntryForm.CurrentEntryForm, dis);
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.currentControl.MoveNext();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var form = CreateNewEntry();
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
            
            var dis = form.Fields[11];
            var dis_value = dis.CreateEntryVariable(26);
            dis_value.Value = 26.ToString();
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();

            var parser = DependencyService.Get<MobileDataKit.Core.Model.Flow.IParser>();
            var res = parser.ParseExpression(dis.PostCondition).Eval(EntryForm.CurrentEntryForm, dis);
            EntryTransaction.DefaultTransaction().CommitAndStartTransaction();
        }
    }
}
