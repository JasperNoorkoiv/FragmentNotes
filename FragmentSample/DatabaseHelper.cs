using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace FragmentSample
{
    public class DatabaseHelper
    {
        SQLiteConnection db;
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myNotes3.db3");

        public DatabaseHelper()
        {
            db = new SQLiteConnection(dbPath);
            CreateDatabaseWithTable();
        }

        public void CreateDatabaseWithTable()
        {
            db.CreateTable<Note>();
        }

        public TableQuery<Note> GetAllNotes()
        {
            return db.Table<Note>();
        }

        public void AddNote(string title, string content)
        {
            Note newNote = new Note();
            newNote.Title = title;
            newNote.CreationTime = DateTime.Now;
            newNote.Content = content;
            db.Insert(newNote);
        }

        public void DeleteNote(int id)
        {
            var note = new Note();
            note.ID = id;
            db.Delete(note);
        }
        public void EditNote(int id, string title, string content)
        {
            var allnotes = GetAllNotes();
            var query = from ord in allnotes
                        where ord.ID == id
                        select ord;
            foreach (Note note in query)
            {
                note.ID = id;
                note.Title = title;
                note.Content = content;
                db.Update(note);
            }
        }
    }
}