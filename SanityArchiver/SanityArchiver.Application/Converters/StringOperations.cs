using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SanityArchiver.DesktopUI.Converters
{
    class StringOperations
    {
        public static string RemoveFileExtension(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            fileName = fileName.Substring(0, fileName.Length - extension.Length);
            return fileName;
        }

        public static string CreateDecryptionFileName(string filePath, double timeOfDecryption)
        {
            string fileName = Path.GetFileName(filePath);
            filePath = filePath.Substring(0, filePath.Length - fileName.Length);
            string newFileName = $"[{fileName}]_[{timeOfDecryption.ToString()}ms].txt";
            filePath += newFileName;
            return filePath;
        }

        public static string GetFilePathWithoutName(string path)
        {
            path = Path.GetDirectoryName(path) + "\\";
            return path;
        }
    }
}
