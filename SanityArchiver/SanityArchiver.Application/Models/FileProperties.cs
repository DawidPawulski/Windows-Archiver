using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SanityArchiver.DesktopUI.Converters;

namespace SanityArchiver.Application.Models
{
    public class FileProperties
    {
        public string FileName { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Size { get; set; }        
        public string FullName { get; set; }
        public bool isHidden { get; set; }
        public string Extension { get; set; }
        public string CheckboxName { get; set; }
        
        public void GetFileName(string path)
        {
            FileName = Path.GetFileNameWithoutExtension(path);
            Extension = Path.GetExtension(path);
            FullName = CheckboxName;
        }

        public void SetExtension(string newExtension, string path)
        {
            Path.ChangeExtension(path, newExtension);
            Extension = Path.GetExtension(FullName);
        }

        public void ChangeName(string newName, string path)
        {
            string fileLocation = StringOperations.GetFilePathWithoutName(path);
            string newNamePath = fileLocation + newName + Extension;
            try
            {
                File.Move(FullName, newNamePath);
            }
            catch (IOException)
            {
                Console.WriteLine("File already exist");
            }
        }

        public void HideFile(string path)
        {
            FileAttributes fileAttributes = File.GetAttributes(path);
            if (isHidden)
            {
                fileAttributes = FileAttributes.Hidden;
                File.SetAttributes(path, fileAttributes);
            }
            else
            {
                fileAttributes = RemoveAttribute(fileAttributes, FileAttributes.Hidden);
                File.SetAttributes(path, fileAttributes);
            }
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }
    }
}
