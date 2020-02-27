using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using SanityArchiver.Application.Models;
using System.Security.AccessControl;

namespace SanityArchiver.DesktopUI.ViewModels
{
    class AtribiutesView
    {

        public static void ShowDialogWindowWithAttributes(string FullName, string fileName, string extension, bool hidden)
        {
            var dialogWindow = new Views.DialogWindow();
            CreateGridLayout(dialogWindow.MainGrid);
            PopulateGrid(dialogWindow.MainGrid, fileName, extension, hidden, dialogWindow, FullName);
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;
            dialogWindow.ShowDialog();
        }

        private static void CreateGridLayout(Grid grid)
        {            
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            RowDefinition row5 = new RowDefinition();
            row1.Height = new GridLength(20, GridUnitType.Pixel);
            row2.Height = new GridLength(0, GridUnitType.Auto);
            row3.Height = new GridLength(0, GridUnitType.Auto);
            row4.Height = new GridLength(0, GridUnitType.Auto);
            row5.Height = new GridLength(20, GridUnitType.Pixel);
            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            ColumnDefinition column3 = new ColumnDefinition();
            ColumnDefinition column4 = new ColumnDefinition();
            ColumnDefinition column5 = new ColumnDefinition();
            column1.Width = new GridLength(20, GridUnitType.Pixel);
            column2.Width = new GridLength(0, GridUnitType.Auto); 
            column3.Width = new GridLength(0, GridUnitType.Auto);
            column4.Width = new GridLength(0, GridUnitType.Auto);
            column5.Width = new GridLength(20, GridUnitType.Pixel);
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);
            grid.RowDefinitions.Add(row4);
            grid.RowDefinitions.Add(row5);
            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);
            grid.ColumnDefinitions.Add(column3);
            grid.ColumnDefinitions.Add(column4);
            grid.ColumnDefinitions.Add(column5);
        }

        private static void PopulateGrid(Grid grid, string fileName, string extension, bool hidden, Views.DialogWindow dialogWindow, string FullName)
        {
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            label1.Content = "File Name";
            label2.Content = "Extension";
            label3.Content = "Hidden";
            TextBox fileNameBox = new TextBox();
            fileNameBox.Text = fileName;
            TextBox extensionBox = new TextBox();
            extensionBox.Text = extension;           
            CheckBox hiddenBox = new CheckBox();
            if (hidden)
            {
                hiddenBox.IsChecked = true;
            }
            else
            {
                hiddenBox.IsChecked = false;
            }
            Button saveButton = new Button();
            Button cancelButton = new Button();
            saveButton.Content = "save";
            cancelButton.Content = "cancel";
            SetButtonsClickEvents(saveButton, cancelButton, dialogWindow, fileNameBox, extensionBox, hiddenBox, FullName);
            Grid.SetRow(saveButton, 3);
            Grid.SetColumn(saveButton, 1);
            Grid.SetRow(cancelButton, 3);
            Grid.SetColumn(cancelButton, 3);
            Grid.SetRow(label1, 1);
            Grid.SetColumn(label1, 1);
            Grid.SetRow(label2, 1);
            Grid.SetColumn(label2, 2);
            Grid.SetRow(label3, 1);
            Grid.SetColumn(label3, 3);
            Grid.SetRow(fileNameBox, 2);
            Grid.SetColumn(fileNameBox, 1);
            Grid.SetRow(extensionBox, 2);
            Grid.SetColumn(extensionBox, 2);
            Grid.SetRow(hiddenBox, 2);
            Grid.SetColumn(hiddenBox, 3);
            grid.Children.Add(fileNameBox);
            grid.Children.Add(extensionBox);
            grid.Children.Add(hiddenBox);
            grid.Children.Add(label1);
            grid.Children.Add(label2);
            grid.Children.Add(label3);
            grid.Children.Add(saveButton);
            grid.Children.Add(cancelButton);
        }

        private static void SetButtonsClickEvents(Button save, Button cancel, Views.DialogWindow dialogWindow, TextBox fileName, TextBox extension, CheckBox hidden, string path)
        {
            cancel.Click += delegate
            {
                dialogWindow.Close();
            };
            save.Click += delegate
            {
                FileInfo fileInfo = new FileInfo(path);
                FileSecurity fileSecurity = fileInfo.GetAccessControl();
                string user = System.Environment.UserName;
                fileSecurity.AddAccessRule(new FileSystemAccessRule(user, FileSystemRights.FullControl, AccessControlType.Allow));
                fileInfo.SetAccessControl(fileSecurity);
                FileProperties file = new FileProperties()
                {
                    CheckboxName = fileInfo.FullName,
                    FileName = fileInfo.Name,
                    CreatedTime = fileInfo.CreationTime,
                    Size = System.String.Format("{0}KB", fileInfo.Length / 1024),
                };
                file.GetFileName(path);
                file.SetExtension(extension.Text, path);
                if (hidden.IsChecked == true)
                {
                    file.isHidden = true;
                }
                else
                {
                    file.isHidden = false;
                }
                file.HideFile(path);
                file.ChangeName(fileName.Text, path);
                dialogWindow.Close();
            };
        }

    }
}
