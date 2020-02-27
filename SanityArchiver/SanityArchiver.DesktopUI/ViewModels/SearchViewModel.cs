using SanityArchiver.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SanityArchiver.DesktopUI.ViewModels
{
    class SearchViewModel
    {
       

        public static bool IsPhraseLongEnough(string phrase)
        {
            if (phrase.Length < 3)
            {
                ShowPopUp();
                return false;
            }
            else
            {
                SanityArchiver.Application.Models.SearchModel.Phrase = phrase;
                return true;
            }
        }

        public static void ShowPopUp()
        {
            MessageBoxResult result = MessageBox.Show("Search phrase must be at least 3 characters long!");
        }

        public static void PopulateWithSearchResult(ListView listView, TextBlock textBlock)
        {
            var FolderContent = new FolderContent();
            var items = new List<FileProperties>();
            List<string> FolderTag = SearchModel.GetAllSearchedFiles();
            foreach (var file in FolderTag)
            {
                FileInfo _dirinfo = new FileInfo(file);
                items.Add(new FileProperties() { CheckboxName = _dirinfo.FullName, FileName = _dirinfo.Name, CreatedTime = _dirinfo.CreationTime, Size = System.String.Format("{0}KB", _dirinfo.Length / 1024) });


            }
            listView.ItemsSource = items;
            textBlock.Text = $"We found {items.Count} files. See Below";
        }
    }
}
