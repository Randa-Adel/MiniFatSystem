using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Delete
    {
        public void delete(string fname)
        {
            int index = Program.CurrentDirectory.SearchDirectory(fname);
            if(index !=-1)
            {
                byte attr = Program.CurrentDirectory.DirectoryTable[index].fileAttribute;
                if (attr == 0x0)
                {
                    int fc = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                    int fsize = Program.CurrentDirectory.DirectoryTable[index].fileSize;
                    FileEntry file = new FileEntry(fname.ToCharArray(), attr,fc,fsize, Program.CurrentDirectory,null);
                    file.DeleteFile();
                }
                else 
                {
                    Console.WriteLine($"The system cannot find the file specified.");
                }
            }
            else
            {
                Console.WriteLine($"The system cannot find the file specified.");
            }
        }
    }
}
