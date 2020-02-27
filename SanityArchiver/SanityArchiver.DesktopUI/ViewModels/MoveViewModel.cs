using SanityArchiver.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SanityArchiver.DesktopUI.ViewModels
{
    public class MoveViewModel
    {
        public static void CopyFile(string sourceFileName, string destFileName, Zip zip)
        {
            File.Copy(sourceFileName, destFileName, true);
            MoveModel.SourceFileName = "";
            zip.PathList.Clear();
            
        }

        public static void MoveFile(string sourceFileName, string destFileName, Zip zip)
        {
            try
            {
                File.Move(sourceFileName, destFileName);
            }
            catch (System.IO.IOException e)
            {

            }
            finally
            {
                MoveModel.SourceFileName = "";
                zip.PathList.Clear();
            }
            
        }
    }
}
