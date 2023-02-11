using System.IO;

namespace Controller
{
    public static class FileController
    {

        public static bool FileIsNotLocked(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName: filePath);

                using (FileStream stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }

        public static string GetFileDirectory(string filePath)
        {
            FileInfo fileInfo = new FileInfo(fileName: filePath);
            return fileInfo.DirectoryName;
        }

        public static string GetFileName(string filePath)
        {
            FileInfo fileInfo = new FileInfo(fileName: filePath);
            return fileInfo.Name;
        }

    }
}
