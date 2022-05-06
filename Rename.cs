using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
     class Rename
    {
        public void rename(string oldname, string newname)
        {
            int oldindex = Program.CurrentDirectory.SearchDirectory(oldname);
            if (oldindex != -1) 
            {
                int newindex = Program.CurrentDirectory.SearchDirectory(newname);
                if (newindex == -1) 
                {
                    DirectoryEntry d = new DirectoryEntry(oldname.ToCharArray(), Program.CurrentDirectory.DirectoryTable[oldindex].fileAttribute,Program.CurrentDirectory.DirectoryTable[oldindex].firstCluster, Program.CurrentDirectory.DirectoryTable[oldindex].fileSize);//هو عباره عن المكان اللي لقيت فيه الاسم القديم
                    d.fileName = Encoding.ASCII.GetBytes(newname);
                    Program.CurrentDirectory.DirectoryTable.RemoveAt(oldindex);
                    Program.CurrentDirectory.DirectoryTable.Insert(oldindex, d);
                    Program.CurrentDirectory.WriteDirctory();
                }
                else
                {
                    Console.WriteLine($"A duplicate file name exists, or the file \n cannot be found.");
                }
            }
            else
            {
                Console.WriteLine($"The system cannot find the file specified.");
            }
        }
    }
}
