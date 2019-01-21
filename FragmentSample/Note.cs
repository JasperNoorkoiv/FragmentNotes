using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using SQLite;
using Android.Widget;

namespace FragmentSample
{
    class Note
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Content { get; set; }
        [MaxLength(255)]
        public DateTime CreationTime { get; set; }
    }
}