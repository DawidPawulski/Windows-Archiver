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
    class FolderTree
    {
        string _FolderPath { get; set; }
        List<FileProperties> FolderFiles { get; set; }

        public void Populate(string header, string tag, TreeView _root, TreeViewItem _child, bool isfile)
        {
            TreeViewItem _driitem = new TreeViewItem();
            _driitem.Tag = tag;
            _driitem.Header = header;
            _driitem.Expanded += new RoutedEventHandler(_driitem_Expanded);
            if (!isfile)
            {
                _driitem.Items.Add(new TreeViewItem());
            }
            if (_root != null)
            {
                _root.Items.Add(_driitem);
            }
            else
            {
                _child.Items.Add(_driitem);
            }
        }


        void _driitem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem _item = (TreeViewItem)sender;
            List<FileProperties> items = new List<FileProperties>();
            try
            {
                if (_item.Items.Count == 1 && ((TreeViewItem)_item.Items[0]).Header == null)
                {
                    _item.Items.Clear();
                    foreach (string dir in Directory.GetDirectories(_item.Tag.ToString()))
                    {
                        DirectoryInfo _dirinfo = new DirectoryInfo(dir);
                        Populate(_dirinfo.Name, _dirinfo.FullName, null, _item, false);
                    }                    
                }
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Odmowa dostepu");
            }

        }
        

    }
}
