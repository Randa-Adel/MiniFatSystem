using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Copy
    {
        public void copy(string s,string d)
        {
            int counter_of_filecopy = 0;
            int sindex = Program.CurrentDirectory.SearchDirectory(s);
            if (sindex!=-1)
            {
                string Input;
                int name_start = d.LastIndexOf('\\');
                string name = d.Substring(name_start + 1);
                int dindex = Program.CurrentDirectory.SearchDirectory(d);
                if (d!=Program.CurrentPath)
                {
                    if (dindex != -1)
                    {
                        Console.WriteLine("Overwrite" + d + "(Yes/No):");
                        Input = Console.ReadLine();
                        if (Input.ToUpper() == "YES")
                        {
                            int fc = Program.CurrentDirectory.DirectoryTable[sindex].firstCluster;
                            int fsize = Program.CurrentDirectory.DirectoryTable[sindex].fileSize;
                            DirectoryEntry DE = new DirectoryEntry(s.ToCharArray(), 0x0, fc, fsize);

                            int dfc = Program.CurrentDirectory.DirectoryTable[dindex].firstCluster;
                            Directory dir = new Directory(d.ToCharArray(), 0x10, dfc, Program.CurrentDirectory);
                            dir.DirectoryTable.Add(DE);
                            Console.WriteLine("\t"+ ++counter_of_filecopy + "file(s) copied.");
                        }
                        else
                        {
                            Console.WriteLine("\t0 file(s) copied.");
                        }
                    }
                    else
                    {
                        int fc = Program.CurrentDirectory.DirectoryTable[sindex].firstCluster;
                        int fsize = Program.CurrentDirectory.DirectoryTable[sindex].fileSize;
                        DirectoryEntry DE = new DirectoryEntry(s.ToCharArray(), 0x0, fc, fsize);
                        Directory dir = new Directory(d.ToCharArray(), 0x10, fc, Program.CurrentDirectory);
                        dir.DirectoryTable.Add(DE);
                        Console.WriteLine("\t"+ ++counter_of_filecopy + " file(s) copied.");
                    }
                }
                else { Console.WriteLine("The file cannot be copied onto itself.");}
            }
            else
            {
                Console.WriteLine("The system cannot find the path specified.");
            }

        }
    }
}
