using System.IO;
using Windows.Storage;
using XamFormsPrototype.Contracts;

namespace XamFormsPrototype.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename) =>
            Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
    }
}
