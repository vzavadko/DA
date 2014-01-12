using System.IO;

namespace XRay
{
    public class FileSystemHelper
    {
        private DirectoryInfo _directoryInfo;
        
        public DirectoryInfo DirectoryInfo
        {
            get { return _directoryInfo; }
            set { _directoryInfo = value; }
        }

        public FileSystemHelper(string path)
        {
            _directoryInfo = new DirectoryInfo(path);
        }

        public void ClearDirectory()
        {
            var fileSystemEntries = DirectoryInfo.GetFileSystemInfos();

            foreach (var entry in fileSystemEntries)
            {
                entry.Delete();
            }
        }
    }
}