using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class ChangeDirectory
    {
        public void cd(string name)
        {
            int index = Program.CurrentDirectory.SearchDirectory(name);
            if (index != -1)    
            {
                byte attr = Program.CurrentDirectory.DirectoryTable[index].fileAttribute;
                if(attr == 0x10)
                {
                    int fc = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                    Directory d = new Directory(name.ToCharArray(), 0x10, fc, Program.CurrentDirectory);
                    Program.CurrentDirectory = d;
                    Program.CurrentPath += "\\" + name;
                    Program.CurrentDirectory.ReadDirectory();
                }
            }
            else
            {
                Console.WriteLine("The system cannot find the path specified.");
            }
        }


    }
}
