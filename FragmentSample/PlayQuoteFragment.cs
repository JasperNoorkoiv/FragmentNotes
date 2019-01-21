using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace FragmentSample
{
    public class PlayQuoteFragment : Fragment
    {
        public int NoteId => Arguments.GetInt("current_note_id", 0);
        DatabaseHelper databaseHelper = new DatabaseHelper();

        public static PlayQuoteFragment NewInstance(int noteId)
        {
            var bundle = new Bundle();
            bundle.PutInt("current_note_id", noteId);
            return new PlayQuoteFragment { Arguments = bundle };
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }
            var view = inflater.Inflate(Resource.Layout.Notesrow, null);

            var switcher = (ViewSwitcher)view.FindViewById(Resource.Id.viewSwitcher1);
            TextView textTitle = view.FindViewById<TextView>(Resource.Id.txtNote);
            TextView textNote = view.FindViewById<TextView>(Resource.Id.txtNoteGlimpse);
            Button editButton = view.FindViewById<Button>(Resource.Id.button_edit);

            // second view items
            TextInputEditText editTitle = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText1);
            TextInputEditText editNote = view.FindViewById<TextInputEditText>(Resource.Id.textInputEditText2);
            Button saveEditButton = view.FindViewById<Button>(Resource.Id.button_save_edit);

            // switch to edit mode
            editButton.Click += delegate { switcher.ShowNext(); };

            // save edited note
            var intent = new Intent(Activity, typeof(MainActivity));

            saveEditButton.Click += delegate
            {
                databaseHelper.EditNote(NoteId + 1, editTitle.Text, editNote.Text);
                StartActivity(intent);
            };
            // display note
            var NoteList = databaseHelper.GetAllNotes().ToList();
            var result = NoteList.Single(s => s.ID == NoteId + 1);
            textTitle.Text = result.Title;
            textNote.Text = result.Content;
            return view;
        }
    }
}