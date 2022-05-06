using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Directory : DirectoryEntry
    {
       
       public List<DirectoryEntry> DirectoryTable = new List<DirectoryEntry>();
        public Directory Parent = null;

        public Directory(char[] name, byte attr, int fcluster, Directory parent) : base(name, attr, fcluster,0)
        {
            if (parent != null)
            {
                this.Parent = parent;
            }
        }
        
        public void WriteDirctory()
        {
           
            byte[] DEB = new byte[32];
            byte[] DTB = new byte[32 * DirectoryTable.Count];
            for (int i = 0; i < DirectoryTable.Count; i++)
            {
                DEB = DirectoryTable[i].getBytes();
                for (int j = i*32; j < 32*(i+1); j++)
                {
                    DTB[j] = DEB[j%32];
                }
            }
            
            double d = Math.Ceiling(DTB.Length / 1024.0);
            int number_of_full_size_block = DTB.Length / 1024;
            int No_Of_RequiredBlocks =(int)d;
            int Remainder = DTB.Length % 1024;
            int FatIndex = 0;
            int LastIndex = -1;
            if (No_Of_RequiredBlocks <= FAT_Table.getAvailableBlocks())
            {
                if(firstCluster != 0)
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
                for (int i = 0; i < DTB.Length; i++)
                {       
                    count[i%1024] = DTB[i];
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
                        arr[i%1024] = DTB[i];
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
                if (this.firstCluster != 0)
                {
                  
                    if (this.DirectoryTable.Count == 0)
                    {
                        FAT_Table.Set_Next(this.firstCluster, 0);
                        this.firstCluster = 0;                      
                    }                
                }
                FAT_Table.WriteFatTable();
            }
            else
            {
                Console.WriteLine("There Is No Avaialble Space");
            }
        }
        public void ReadDirectory()
        {       
            List<byte> ls = new List<byte>();
            byte[] b = new byte[32];
            int FatIndex = 0;
            int Next= 0;
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
                for (int i = 0; i < ls.Count; i++)  
                {        
                    b[i % 32] = ls[i];
                    if ((i + 1) % 32 == 0)
                    {
                        DirectoryEntry d = getDirectoryEntry(b);
                        if (d.fileName[0] != (byte)'\0')
                        {
                            DirectoryTable.Add(d);
                        }                     
                    }                           
                }
           }
        }
        void cleanDirectoryTable()
        {
            for (int i = 0; i < (DirectoryTable.Count);i++)
            {
                if (DirectoryTable[i].fileName[0] == 0 )
                {
                    DirectoryTable.RemoveAt(i);
                  
                }
                if (i < DirectoryTable.Count - 1)
                {
                    if (DirectoryTable[i].fileName.Equals(DirectoryTable[i + 1].fileName))
                    {
                        DirectoryTable.RemoveAt(i);
                    }
                }
               
            }
        }
        public int SearchDirectory(string fileName)
        {
            ReadDirectory();
            cleanDirectoryTable();
            for (int i = 0; i < DirectoryTable.Count; i++)
            {
               char[] FN = new char[DirectoryTable[i].fileName.Length];
                for (int j = 0; j < FN.Length; j++)
                {
                    FN[j] = (char)DirectoryTable[i].fileName[j];
                }                
                string convert = "";
                for (int k = 0; k < FN.Length; k++)
                {
                    convert += FN[k];
                }
                if (fileName==convert)
                {
                   
                    return i;
                }

            }
            return -1;   
        }
        public void UpdateContent(DirectoryEntry d)
        {   
            ReadDirectory();
            int index = SearchDirectory(Encoding.ASCII.GetString(d.fileName, 0, d.fileName.Length));
            {
                DirectoryTable.RemoveAt(index);
                DirectoryTable.Insert(index, d);
                WriteDirctory();  
            }
        }    
        public void DeleteDirectory()
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
                
                if (Parent!=null)
                {
                    Parent.ReadDirectory();
                    int I = Parent.SearchDirectory(Encoding.ASCII.GetString(fileName, 0,fileName.Length));
                    if (I!=-1)
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