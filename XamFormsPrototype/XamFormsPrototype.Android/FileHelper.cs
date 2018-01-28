using System;
using System;
using System.IO;
using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
