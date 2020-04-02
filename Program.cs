using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReScoreThemis
{
    class Program
    {
        static void Main(string[] args)
        {
            string SourcePath = "", TagetPath = "";
            Console.WriteLine("Path for source code: ");
            SourcePath = Console.ReadLine();
            Console.WriteLine("Path of Themis logs folder: ");
            TagetPath = Console.ReadLine();
            if (TagetPath == "") TagetPath = SourcePath;
            bool exist = Directory.Exists(SourcePath);
            if (!exist)
            {
                Console.WriteLine("The source path isn't existed. Close Program");
                Console.WriteLine("Press enter key to close ...");
                Console.ReadLine();
                return;
            }
            else
            {
                if (Directory.Exists(TagetPath + "\\Logs")) Directory.Delete(TagetPath + "\\Logs", true);
                ReScore(SourcePath, TagetPath);
            }
        }
        public static void ReScore(string ParentPath, string Taget)
        {
            try
            {
                IEnumerable<string> enums = Directory.EnumerateDirectories(ParentPath);
                List<string> dirs = new List<string>(enums);
                foreach (var dir in dirs)
                {
                    string Contestant_Name = Path.GetFileName(dir);
                    IEnumerable<string> enums_contestant = Directory.GetFiles(dir);
                    List<string> codes = new List<string>(enums_contestant);
                    foreach (var code in codes)
                    {
                        string Problem_Name = Path.GetFileNameWithoutExtension(code);
                        string extention = Path.GetExtension(code);
                        string new_name = "[" + Contestant_Name + "][" + Problem_Name + "]." + extention;
                        try
                        {
                            if (!Directory.Exists(Taget + "\\" + new_name)) File.Copy(code, Taget + "\\" + new_name);
                        }
                        catch (UnauthorizedAccessException e)
                        {
                            Console.WriteLine("Can not access directory: " + Taget);
                            Console.WriteLine(e.Message);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("Can not access directory: " + ParentPath);
                Console.WriteLine(e.Message);
            }
        }
    }
}
