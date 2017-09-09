using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MobileDataKit_Collect
{
	public class SectionView  
   {
  //      public static Realms.Realm realm = null;
  //      private Realms.Transaction transaction =null;
  //      private Model.Section Section;
  //      System.Timers.Timer timer;
  //      public SectionView ( Model.Section section)
		//{
  //          this.timer = new System.Timers.Timer();
  //          timer.Interval = 100;
  //          timer.Elapsed += Timer_Elapsed;
  //          realm = Realms.Realm.GetInstance();
  //          this.BackgroundColor = Color.White;
            
       
  //          this.Section = section;
  //          this.Disappearing += SectionView_Disappearing;
  //       this.CurrentPageChanged += SectionView_CurrentPageChanged;
  //          using(var tr = realm.BeginWrite())
  //          {
  //              foreach (var r in section.Fields)
  //              {
  //              if(string.IsNullOrWhiteSpace(r.FieldID))
  //                  this.Children.Add((new EntryControls.ControlFactory(r)).Create());
  //                  return;
                  
  //              }

  //              tr.Commit();
  //          }



  //          this.transaction = SectionView.realm.BeginWrite();

  //          // = dddddddddddddddddddddddddddddddddd;
  //      }


  //      private List<EntryPage> Children = new List<EntryPage>();


  //      private int cou = 0;
  //      private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
  //      {
           
  //              this.timer.Stop();
  //          return;
            
  //          //var baseco = (EntryControls.BaseControl)this.CurrentPage;

  //          //var nextfield = baseco.Field;
  //          //try
  //          //{
  //          //    var created = false;
  //          //    while (!created)
  //          //    {
  //          //        nextfield = this.Section.Fields[this.Section.Fields.IndexOf(nextfield) + 1];
  //          //        if (string.IsNullOrWhiteSpace(nextfield.FieldID))
  //          //        {
  //          //            this.Children.Add(new EntryControls.ControlFactory(nextfield).Create());
  //          //            created = true;
  //          //        }

  //          //    }

  //          //}
  //          //catch (Exception exxdxe)
  //          //{
  //          //    var fdfdfd = exxdxe;

  //          //}

  //          //if(!running)
  //          //{
  //          //    running = true;
  //          //    while (cou < 5)
  //          //    {
  //          //        try
  //          //        {
  //          //            this.Timer_Elapsed(sender, e);
  //          //        }
  //          //        catch
  //          //        {

  //          //        }

  //          //        cou = cou + 1;
  //          //    }
  //          //}
           
  //      }

  //      private Boolean running = false;
  //      private void SectionView_CurrentPageChanged(object sender, EventArgs e)
  //      {
  //          var dddf = e;
  //      }

  //      private void SectionView_Disappearing(object sender, EventArgs e)
  //      {
  //          var dddf = e;
  //      }

  //      private ContentPage PreviousPage = null;
       
  //      private bool _ReturningBack = false;

  //      protected override void OnCurrentPageChanged()
  //      {



  //          if (_ReturningBack)
  //          {
  //              _ReturningBack = false;
  //              return;
  //          }

  //          var fffffffffffffff = this.PreviousPage;
  //          if (fffffffffffffff != null)
  //          {
  //              if (this.Children.IndexOf(this.CurrentPage) < this.Children.IndexOf(this.PreviousPage))
  //              {



  //                  //returning back;
  //                  _ReturningBack = true;

  //                  this.CurrentPage = this.PassedControls.Pop();
  //                  this.PreviousPage = this.CurrentPage;
  //                  return;
  //              }

  //              var d = (EntryControls.BaseControl)fffffffffffffff;
  //              var x = d.LeaveControl();
  //              if (x == false)
  //              {
  //                  _ReturningBack = true;
  //                  this.CurrentPage = fffffffffffffff;
  //                  return;
  //              }
  //              else
  //              {
  //                  var ffff = GetNextControlInsteadOfThisPage((EntryControls.BaseControl)this.CurrentPage);
  //                  if (ffff != null)
  //                  {
  //                      _ReturningBack = true;
  //                      this.CurrentPage = ffff;

  //                  }

  //                  if (this.transaction != null)
  //                      this.transaction.Commit();
  //                  this.transaction = SectionView.realm.BeginWrite();
  //              }



  //          }
  //          Model.EntryForm.CurrentEntryForm.CurrentVariable = ((EntryControls.BaseControl)this.CurrentPage).ID;
  //          this.PreviousPage = this.CurrentPage;
  //          this.PassedControls.Push((EntryControls.BaseControl)this.CurrentPage);

  //          base.OnCurrentPageChanged();

  //          cou = 0;
  //          running = false;
  //          timer.Start();

  //      }



  //      private Stack<EntryControls.BaseControl> PassedControls = new Stack<EntryControls.BaseControl>();



 
       
  }
}
