using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SanityArchiver.DesktopUI.Converters;
using SanityArchiver.Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO.Compression;
using Microsoft.VisualBasic;
using SanityArchiver.DesktopUI.ViewModels;


namespace SanityArchiver.DesktopUI.Views
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModels.Zip Zip = new Zip();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        private string[] paths = new string[1]; 
        bool ischecked = false;


        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void folders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var FolderContent = new FolderContent();
            string FolderTag = ((System.Windows.FrameworkElement)folders.SelectedItem).Tag.ToString();
            MoveModel.SetDestinationDirectory(FolderTag);
            SelectedFolderContain.ItemsSource = FolderContent.GetAllFiles(FolderTag);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var FolderTree = new FolderTree();
            foreach (DriveInfo driv in DriveInfo.GetDrives())
            {
                if (driv.IsReady)
                {
                    FolderTree.Populate(driv.VolumeLabel + "(" + driv.Name + ")", driv.Name, folders, null, false);

                }
            }
        }
        private void isFileSelected(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            var dataContext = (Application.Models.FileProperties)checkBox.DataContext;
            string filePath = dataContext.CheckboxName;
            string fileName = dataContext.FileName;
            MoveModel.File = $"{fileName}.txt";
            MoveModel.SourceFileName = filePath;
            CopyFile.IsEnabled = true;
            MoveFile.IsEnabled = true;
            paths[0] = filePath;
            ischecked = true;
            FolderContent.CheckIfEncryptable(filePath, Encrypt);
            if(dataContext.Extension == ".txt")
            {
                Open.Visibility = Visibility.Visible;
               
            }
            FolderContent.ListPathManipulation(filePath, checkBox, Zip);
            FolderContent.CheckIfCompressable(Zip.PathList, Compress);
            //FolderContent.CheckIfCompressable(Zip.PathList, Compress);
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var dataContext = (Application.Models.FileProperties)item.DataContext;
            AtribiutesView.ShowDialogWindowWithAttributes(dataContext.FullName, dataContext.FileName, dataContext.Extension, dataContext.isHidden);
        }        

        private void SearchButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (SearchViewModel.IsPhraseLongEnough(SearchInput.Text))
            {
                SearchViewModel.PopulateWithSearchResult(SelectedFolderContain, SearchCountResult);
            }
        }

        private void SearchInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (SearchViewModel.IsPhraseLongEnough(SearchInput.Text))
                {
                    SearchViewModel.PopulateWithSearchResult(SelectedFolderContain, SearchCountResult);
                }
            }
        }

        private void CopyFile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            if (MoveModel.CopyClickCounter == 0)
            {
                CopyFile.Content = "Paste";
                MoveModel.CopyClickCounter = 1;
            }
            else if (MoveModel.CopyClickCounter == 1)
            {
                CopyFile.Content = "Copy";
                CopyFile.IsEnabled = false;
                MoveFile.IsEnabled = false;
                MoveModel.CopyClickCounter = 0;
                MoveViewModel.CopyFile(MoveModel.SourceFileName, MoveModel.DestinantionDirectory, Zip);
            }
            
        }

        private void MoveFile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MoveModel.MoveClickCounter == 0)
            {
                MoveFile.Content = "Paste";
                MoveModel.MoveClickCounter = 1;
            }
            else if (MoveModel.MoveClickCounter == 1)
            {
                MoveFile.Content = "Move";
                MoveFile.IsEnabled = false;
                CopyFile.IsEnabled = false;
                MoveModel.MoveClickCounter = 0;
                MoveViewModel.MoveFile(MoveModel.SourceFileName, MoveModel.DestinantionDirectory, Zip);
            }

        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            ViewModels.TextFileDisplay.ShowDialogWithText(paths[0]);
            ischecked = false;
        }
        private void CompressAction(object sender, RoutedEventArgs e)
        {
            Zip.AskForArchiveName();

        }
    }

}
