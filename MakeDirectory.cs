using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class MakeDirectory
    {
        public void md(string name)
        {                   
            if (name.Length>3&&name.Substring(name.Length - 4) == ".txt")
            {
                if (Program.CurrentDirectory.SearchDirectory(name) == -1)
                {
                    int x = FAT_Table.Available_Block_In_FatTable();
                    DirectoryEntry d = new DirectoryEntry(name.ToCharArray(),0x0, x, 0);

                    Program.CurrentDirectory.DirectoryTable.Add(d);
                    Program.CurrentDirectory.WriteDirctory();
                    if (Program.CurrentDirectory.Parent != null)
                    {
                        Program.CurrentDirectory.UpdateContent(Program.CurrentDirectory.getDirectoryEntry());
                        Program.CurrentDirectory.Parent.WriteDirctory();
                    }
                }
                else
                {
                    Console.WriteLine($"A subdirectory or file {name} already exists.");
                }
            }
            else
            {
                if (Program.CurrentDirectory.SearchDirectory(name) == -1)
                {
                    int x = FAT_Table.Available_Block_In_FatTable();
                    DirectoryEntry d = new DirectoryEntry(name.ToCharArray(), 0x10, x, 0);

                    Program.CurrentDirectory.DirectoryTable.Add(d);
                    Program.CurrentDirectory.WriteDirctory();
                    if (Program.CurrentDirectory.Parent != null)
                    {
                        Program.CurrentDirectory.UpdateContent(Program.CurrentDirectory.getDirectoryEntry());
                        Program.CurrentDirectory.Parent.WriteDirctory();
                    }
                    // FAT_Table.PrintFatTable();
                }
                else
                {
                    Console.WriteLine($"A subdirectory or file {name} already exists.");
                }
            }
        }

    }
}
