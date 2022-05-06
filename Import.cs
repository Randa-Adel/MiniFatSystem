using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime;
namespace Shell
{
    class Import
    {
        public void import(string path)
        {
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                int size = content.Length;
                int name_start = path.LastIndexOf('\\');
                string name = path.Substring(name_start+1);
                int index = Program.CurrentDirectory.SearchDirectory(name);
                if(index == -1)
                {                 
                    if(size > 0 )
                    {
                        FileEntry file = new FileEntry(name.ToCharArray(), 0x0, FAT_Table.Available_Block_In_FatTable(), size,Program.CurrentDirectory, content);
                        file.WriteFile();
                        DirectoryEntry dir = new DirectoryEntry(name.ToCharArray(), 0x0, FAT_Table.Available_Block_In_FatTable(), size);
                        Program.CurrentDirectory.DirectoryTable.Add(dir);
                        Program.CurrentDirectory.WriteDirctory();
                    }
                    else
                    {
                        FileEntry file = new FileEntry(name.ToCharArray(), 0x0, 0,0 ,Program.CurrentDirectory, content);
                        file.WriteFile();
                    }
                }
                else
                {
                    Console.WriteLine("The File Already Exist");
                }
            }
            else
            {
                Console.WriteLine("This File Not Exist");
            }
        }
    }
}
