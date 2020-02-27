using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace SanityArchiver.DesktopUI.ViewModels
{
    public class Zip
    { 
        public string Record { set; get; }
        
        public List<string> PathList = new List<string>();
        
        

        public void AskForArchiveName()
        {
            
            var dialogWindow = new Views.ZipBox();
            dialogWindow.SelectedFiles = PathList;
            if (dialogWindow.ShowDialog() == true)
            {
                


            }


        }
    }
}
