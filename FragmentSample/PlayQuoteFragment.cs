using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite;

namespace FragmentSample
{
    public class PlayQuoteFragment : Fragment
    {
        public int NoteId => Arguments.GetInt("current_note_id", 0);
        DatabaseHelper databaseHelper = new DatabaseHelper();

        EditText editNote;
        EditText editTitle;

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
            var view = inflater.Inflate(Resource.Layout.EditNotes, null);

            var switcher = (ViewSwitcher)view.FindViewById(Resource.Id.viewSwitcher1);
            TextView textTitle = view.FindViewById<TextView>(Resource.Id.txtNote);
            TextView textNote = view.FindViewById<TextView>(Resource.Id.txtNoteGlimpse);
            Button editButton = view.FindViewById<Button>(Resource.Id.button_edit);
            Button deleteButton = view.FindViewById<Button>(Resource.Id.button_delete);

            // second view items
            EditText editTitle = view.FindViewById<EditText>(Resource.Id.txtNote);
            EditText editNote = view.FindViewById<EditText>(Resource.Id.txtNoteGlimpse);
            if (editTitle != null)
            {
                this.editTitle = editTitle;
                this.editNote = editNote;
            }

            Button saveEditButton = view.FindViewById<Button>(Resource.Id.button_edit);

            // switch to edit mode
            editButton.Click += delegate {
                switcher.ShowNext();
                var allNotes = databaseHelper.GetAllNotes();
                var curNote = allNotes.ElementAt(NoteId);
                EditButton_Clicked(curNote);
            };

            // save edited note
            var intent = new Intent(Activity, typeof(MainActivity));



            // delete note
            deleteButton.Click += delegate
            {
                var allNotes = databaseHelper.GetAllNotes();
                var curNote = allNotes.ElementAt(NoteId);
                databaseHelper.DeleteNote(curNote.ID); StartActivity(intent);
            };
            var NoteList = databaseHelper.GetAllNotes().ToList();
            var result = NoteList[NoteId];
            textTitle.Text = result.Title;
            textNote.Text = result.Content;
            return view;
        }

        //edit note
        public void EditButton_Clicked(Note note)
        {
            var intent = new Intent(Activity, typeof(MainActivity));

            note.Title = editTitle.Text;
            note.Content = editNote.Text;

            databaseHelper.EditNote(note);
            StartActivity(intent);
        }
    }
}