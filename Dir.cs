using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Dir
    {
        public void dir()
        {
            int fileCounter = 0;
            int directoryCounter = 0;
            int fileSizeCounter = 0;
            Console.WriteLine("Directory of : "+Program.CurrentPath+"\\");
            for (int i = 0; i < Program.CurrentDirectory.DirectoryTable.Count; i++)
            {
                byte attr = Program.CurrentDirectory.DirectoryTable[i].fileAttribute;
                if (attr==0x0)
                {
                    Console.WriteLine("\t\t"+(Program.CurrentDirectory.DirectoryTable[i].fileSize).ToString() +" " + Encoding.Default.GetString(Program.CurrentDirectory.DirectoryTable[i].fileName));
                    fileCounter++;
                    fileSizeCounter += Program.CurrentDirectory.DirectoryTable[i].fileSize;
                }
                else
                {
                    Console.WriteLine(" <DIR> \t\t"+ Encoding.Default.GetString(Program.CurrentDirectory.DirectoryTable[i].fileName));
                    directoryCounter++;
                }
            }
            Console.WriteLine(" "+fileCounter.ToString() + "  File(s)  " + fileSizeCounter.ToString() + "  bytes");
            Console.WriteLine(" "+directoryCounter.ToString() +"  Dir(s)  "+ FAT_Table.getFreeSpace() + "  bytes free");
        }
    }
}
