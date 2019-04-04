using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

namespace FragmentSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme" )]
    public class MainActivity : AppCompatActivity
    {   
        DatabaseHelper databaseHelper = new DatabaseHelper();
        List<string> TitleList = new List<string>(Titles);
        List<string> NoteList = new List<string>(Notes);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //AppCenter.Start("5bdb87e5-6c27-4009-9226-fa2b92e0beb0", typeof(Distribute));
            //AppCenter.Start("5bdb87e5-6c27-4009-9226-fa2b92e0beb0", typeof(Analytics), typeof(Crashes));
            AppCenter.Start("75171bf8-d9bf-4172-88e1-8e5dbaddbbc6",
            typeof(Analytics), typeof(Crashes));
            AppCenter.Start("75171bf8-d9bf-4172-88e1-8e5dbaddbbc6", typeof(Analytics), typeof(Crashes));
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);



            if (!File.Exists(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myNotes2.db3")))
            {
                databaseHelper.CreateDatabaseWithTable();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.AddNote)
            {
                StartActivity(typeof(CreateNotesActivity));
                Toast.MakeText(this, "Write your note and press the button again.", ToastLength.Short).Show();
                return true;
            }
            return base.OnOptionsItemSelected(item);

        }
        public static string[] Titles = {    };


        public static string[] Notes = {  };
    }
}