using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class FileEntry : DirectoryEntry
    {
        Directory Parent;
       public string Content;
        public FileEntry(char[] name, byte attr, int fcluster,int fSize, Directory parent,string content) : base(name, attr, fcluster,fSize)
        {
            if (parent != null)
            {
                this.Parent = parent;
            }
            this.Content = content;
        }
        public void WriteFile()
        {
            byte[] bytes = Encoding.ASCII.GetBytes(Content);
            double d = Math.Ceiling(bytes.Length / 1024.0);
            int number_of_full_size_block = bytes.Length / 1024;
            int No_Of_RequiredBlocks = (int)d;
            int Remainder = bytes.Length % 1024;
            int FatIndex = 0;
            int LastIndex = -1;
            if (No_Of_RequiredBlocks <= FAT_Table.getAvailableBlocks())
            {
               if (firstCluster != 0)
                {
                    FatIndex = firstCluster;
                }
                else
                {
                    FatIndex = FAT_Table.Available_Block_In_FatTable();
                    firstCluster = FatIndex;
                }
                byte[] count = new byte[1024];
                List<byte[]> Block = new List<byte[]>();
                for (int i = 0; i < bytes.Length; i++)
                {
                    count[i % 1024] = bytes[i];
                    if ((i + 1) % 1024 == 0)
                    {
                        Block.Add(count);
                    }
                }
                if (Remainder > 0)
                {
                    byte[] arr = new byte[1024];
                    int start = number_of_full_size_block * 1024;
                    for (int i = start; i < (start + Remainder); i++)
                    {
                        arr[i % 1024] = bytes[i];
                    }
                    Block.Add(arr);
                }
                for (int i = 0; i < Block.Count; i++)
                {

                    Virtual_Disk.WriteBlock(Block[i], FatIndex);
                    FAT_Table.Set_Next(FatIndex, -1);
                    if (LastIndex != -1)
                    {
                        FAT_Table.Set_Next(LastIndex, FatIndex);
                    }
                    LastIndex = FatIndex;
                    FatIndex = FAT_Table.Available_Block_In_FatTable();
                }
                FAT_Table.WriteFatTable();
            }
            else
            {
                Console.WriteLine("There Is No Avaialble Space");
            }
           
        }
        public void ReadFile()
        {
            List<byte> ls = new List<byte>();
            int FatIndex = 0;          
            int Next = 0;
           // Console.WriteLine("first cluster of file "+firstCluster);
            if (this.firstCluster != 0 && FAT_Table.Get_Next(this.firstCluster) != 0)
            {                
                FatIndex = firstCluster;   
                Next = FAT_Table.Get_Next(FatIndex);
                do
                {       
                    ls.AddRange(Virtual_Disk.Get_Block(FatIndex));
                    FatIndex = Next;
                    if (FatIndex != -1)
                    {
                        Next = FAT_Table.Get_Next(FatIndex);
                    }
                   
                } while (Next != -1);
                string s = "";
                for (int i = 0; i < ls.Count; i++)
                {
                   
                    s += (char)ls[i];
                }
                this.Content=s;
            }

        }
        public void DeleteFile()
        {
            if (firstCluster != 0)
            {
                int index = firstCluster;
                int next = FAT_Table.Get_Next(index);
                do
                {
                    FAT_Table.Set_Next(index, 0);
                    index = next;
                    if (index != -1)
                    {
                        next = FAT_Table.Get_Next(index);
                    }
                } while (index != -1);
                if (Parent != null)
                {
                    Parent.ReadDirectory();
                    int I = Parent.SearchDirectory(Encoding.ASCII.GetString(fileName, 0, fileName.Length));
                    if (I != -1)
                    {
                        Parent.DirectoryTable.RemoveAt(I); 
                        Parent.WriteDirctory(); 
                    }
                }
                FAT_Table.WriteFatTable();
            }

        }
    }
}
