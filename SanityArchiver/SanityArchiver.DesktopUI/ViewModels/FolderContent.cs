using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SanityArchiver.Application.Models;
using System.Windows;

namespace SanityArchiver.DesktopUI.ViewModels
{
    class FolderContent
    {
        
        public List<FileProperties> GetAllFiles(string FolderPath)
        {
            var items = new List<FileProperties>();
            try
            {
                foreach (string dir in Directory.GetFiles(FolderPath))
                {
                    FileInfo _dirinfo = new FileInfo(dir);
                    FileProperties file = new FileProperties()
                    {
                        CheckboxName = _dirinfo.FullName,
                        FileName = _dirinfo.Name,
                        CreatedTime = _dirinfo.CreationTime,
                        Size = System.String.Format("{0}KB", _dirinfo.Length / 1024),
                    };
                    items.Add(file);
                    file.GetFileName(_dirinfo.FullName);

                }
                return items;
            }
            catch(System.UnauthorizedAccessException)
            {
                return null;
            }
            
        }

        public static void CheckIfEncryptable(string filePath, Button encryptButton)
        {
            if (filePath.Contains(".txt"))
            {
                encryptButton.Content = "Encrypt";
                encryptButton.Visibility = Visibility.Visible;
                encryptButton.Click += (sender, e) =>
                {
                    Encrypt.EncryptFile(filePath);
                    encryptButton.Visibility = Visibility.Hidden;
                };
            }
            if (filePath.Contains(".ENC"))
            {
                encryptButton.Content = "Decrypt";
                encryptButton.Visibility = Visibility.Visible;
                encryptButton.Click += (sender, e) =>
                {
                    Encrypt.DecryptFile(filePath);
                    encryptButton.Visibility = Visibility.Hidden;
                };
            }
        }
        public static void ListPathManipulation(string FilePath,CheckBox checkBox,Zip Zip)
        {
           if (checkBox.IsChecked.Equals(true))
           {
                Zip.PathList.Add(FilePath);                
           }
           if (checkBox.IsChecked.Equals(false))
            {             
              Zip.PathList.Remove(FilePath);
                
                    
            }
        }
        public static void CheckIfCompressable(List<string> _selectedFilesPath,Button zipbutton)
        {
            if(_selectedFilesPath.Count > 0)
            {
                if (_selectedFilesPath.Count == 1 & _selectedFilesPath.Contains(".zip"))
                {
                    zipbutton.Content = "Unzip";
                    zipbutton.Visibility = Visibility.Visible;
                    zipbutton.Click += (sender, e) =>
                    {
                        //ZipFileCreator.DecompressFile(_selectedFilesPath);
                        zipbutton.Visibility = Visibility.Hidden;
                    };
                }
                else
                {
                    zipbutton.Content = "Compress";
                    zipbutton.Visibility = Visibility.Visible;
                    zipbutton.Click += (sender, e) =>
                    {
                        
                        zipbutton.Visibility = Visibility.Hidden;
                    };
                }
               
            }
            else
            {
                zipbutton.Visibility = Visibility.Hidden;
            }
        }

        private static void Zipbutton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
