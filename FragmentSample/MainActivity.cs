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

namespace FragmentSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        DatabaseHelper databaseHelper = new DatabaseHelper();
        List<string> TitleList = new List<string>(Titles);
        List<string> NoteList = new List<string>(Notes);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
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
    }
}