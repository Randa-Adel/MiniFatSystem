using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace Shell
{
    class DirectoryEntry
    {
        public byte[] fileName = new byte[11];
        public byte fileAttribute;
        public byte[] fileEmpty = new byte[12];
        public int fileSize;
        public int firstCluster;
        
        public DirectoryEntry(char[] name, byte attr, int fcluster,int fSize)
        {
            int c = 4;
            int len=name.Length;
            if (fileAttribute == 0x10)
            {
                if (name.Length < 11)
                {
                    fileName = Encoding.ASCII.GetBytes(name);
                }
                else
                {
                    char[] newname = new char[11];
                    for (int i = 0; i < newname.Length; i++)
                    {
                        newname[i] += name[i];
                    }
                    fileName = Encoding.ASCII.GetBytes(newname);
                }
            }
            else
            {
                if (name.Length < 11)
                {
                    fileName = Encoding.ASCII.GetBytes(name);
                }
                else
                {
                    char[] newname = new char[11];
                    for (int i = 0; i <=6; i++)
                    {
                        newname[i] += name[i];
                    }
                    for (int i = 7; i <11; i++)
                    {
                        newname[i] += name[len-c];
                        c--;
                    }
                    fileName = Encoding.ASCII.GetBytes(newname);
                }
            }
            fileAttribute = attr;          
            firstCluster = fcluster;
            fileSize = fSize;
        }

        // وظيفتها تاخد الديركتوري انتري الواحد وترجعلي البايتس اللي فيه
        public byte[] getBytes()
        {
            byte[] b = new byte[32];
            byte[] fc = BitConverter.GetBytes(firstCluster);
            byte[] fz = BitConverter.GetBytes(fileSize);
            for (int i = 0; i < 32; i++)
            {
                if (i < 11)
                {
                    if(i<fileName.Length) b[i] = fileName[i];
                }
                else if(i == 11)
                {
                    b[i] = fileAttribute;
                }
                else if(i>=12 && i<24)
                {
                    b[i] = fileEmpty[i-12];
                }
                else if(i>=24 && i<28)
                {
                    b[i] = fc[i - 24];
                }
                else if (i >= 28 && i < 32)
                {
                    b[i] = fz[i - 28];
                }
            }
           return b;
        }
        // وظيفتها تاخد ارراي من البايتس وتكتبهم في مكونات الديركتوري انتري 
        public DirectoryEntry getDirectoryEntry(byte[] b)
        {
            byte[] fc = BitConverter.GetBytes(firstCluster);
            byte[] fz = BitConverter.GetBytes(fileSize);
           
            for (int i = 0; i < 32; i++)
            {
                if (i < 11)
                {                  
                    if (i < fileName.Length)
                    {
                        fileName[i] = b[i];
                    }
                   
                }
                else if (i == 11)
                {
                    fileAttribute = b[i] ;
                }
                else if (i >= 12 && i < 24)
                {
                    fileEmpty[i - 12] = b[i] ;
                }
                else if (i >= 24 && i < 28)
                {
                    fc[i - 24]  = b[i] ;
                }
                else if (i >= 28 && i < 32)
                {
                    fz[i - 28] = b[i] ;
                }
            }
            firstCluster = BitConverter.ToInt32(fc, 0);
            fileSize = BitConverter.ToInt32(fz, 0);
            return this;
        }

        public DirectoryEntry getDirectoryEntry()
        {

            return this;
        }
    }
}