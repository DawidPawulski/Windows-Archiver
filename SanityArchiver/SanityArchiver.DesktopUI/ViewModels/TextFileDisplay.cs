using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace SanityArchiver.DesktopUI.ViewModels
{
    class TextFileDisplay
    {
        public static void ShowDialogWithText(string path)
        {
            var dialogWindow = new Views.DialogWindow();
            CreateLayout(dialogWindow.MainGrid, path);
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;
            dialogWindow.ShowDialog();

        }

        private static void CreateLayout(Grid grid, string path)
        {
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            row1.Height = new GridLength(20, GridUnitType.Pixel);
            row2.Height = new GridLength(0, GridUnitType.Auto);
            row3.Height = new GridLength(20, GridUnitType.Pixel);
            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            ColumnDefinition column3 = new ColumnDefinition();
            column1.Width = new GridLength(20, GridUnitType.Pixel);
            column2.Width = new GridLength(0, GridUnitType.Auto);
            column3.Width = new GridLength(20, GridUnitType.Pixel);
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);
            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);
            grid.ColumnDefinitions.Add(column3);
            TextBlock textBlock = new TextBlock();
            WriteTextToTextBlock(path, textBlock);
            Grid.SetColumn(textBlock, 1);
            Grid.SetRow(textBlock, 1);
            grid.Children.Add(textBlock);
        }

        public static void WriteTextToTextBlock(string path, TextBlock textBlock)
        {
            Encoding encoding = Encoding.UTF8;
            string text = "";
            string [] textLines = File.ReadAllLines(path, encoding);
            foreach(string line in textLines)
            {
                text += line + "\n";
            }
            textBlock.Text = text;
        }
    }
}
