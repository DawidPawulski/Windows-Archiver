using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SanityArchiver.Application.Models
{
    public class SearchModel
    {

        public static string Phrase { get; set; } = "";

        public static List<string> GetAllSearchedFiles()
        {
            string[] allfiles = Directory.GetFiles("/", "*.*", SearchOption.AllDirectories);
            List<FileInfo> fileInfos = new List<FileInfo>();
            List<string> vs = new List<string>();
            foreach (var file in allfiles)
            {
                FileInfo info = new FileInfo(file);

                string reg = Phrase;
                if (Regex.IsMatch(info.Name, reg, RegexOptions.IgnoreCase))
                {
                    vs.Add(file);
                }
            }
            return vs;
        }



    }
}
