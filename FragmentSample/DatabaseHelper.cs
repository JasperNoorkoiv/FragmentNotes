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
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myNotes4.db3");

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
            db.Delete<Note>(id);
        }
        public void EditNote(Note note)
        {
           
             db.Update(note);
           
        }
    }
}