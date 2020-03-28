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
            string MainPath = "";
            Console.WriteLine("Main Path for source code: ");
            MainPath = Console.ReadLine();
            bool exist = Directory.Exists(MainPath);
            if (!exist)
            {
                Console.WriteLine("The path isn't existed. Close Program");
                Console.WriteLine("Press enter key to close ...");
                Console.ReadLine();
                return;
            }
            else
            {
                ReScore(MainPath);
            }
        }
        public static void ReScore(string ParentPath)
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
                        File.Copy(code, ParentPath + "\\" + new_name);
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
