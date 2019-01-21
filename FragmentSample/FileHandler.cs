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

namespace FragmentSample
{
    public class FileHandler
    {
        public static string GetPath(string filename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return filePath;
        }
        public static void Write(string path, string content)
        {
            File.AppendAllText(path, content + System.Environment.NewLine);
        }
    }
}
