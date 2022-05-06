using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Shell
{
    class FAT_Table
    {
        static public int[] fatTable = new int[1024];      
        static  public void InitializeFatTable()
        {
            for (int i = 0; i < 1024; i++)
            {
                if (i < 5)
                {
                    fatTable[i] = -1;
                }
                else
                {
                    fatTable[i] = 0;
                }
            }
        }
        // وظيفة الفانكشن دي انها بتكتب الفات تابل في المكان المخصص له في الفايل في جزئية الميتا داتا
        static  public void WriteFatTable()
        {          
            FileStream stream = new FileStream(@"F:\\Shell Randa\\Shell\\Shell\\Virtual_Disk.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);          
            stream.Seek(1024, SeekOrigin.Begin);
            byte[] result = new byte[fatTable.Length * sizeof(int)];
            Buffer.BlockCopy(fatTable, 0, result, 0, fatTable.Length);
            for (int i = 0; i < result.Length; i++)
            {
                stream.Write(result,i, 1);
            }
            stream.Close();
        }
        //بتقرا الفات تابل من الفايل وترجعهولي في ارراي 
        static public void GetFatTable()
        {
            byte[] result = new byte[1024*4];
            FileStream stream = new FileStream(@"F:\\Shell Randa\\Shell\\Shell\\Virtual_Disk.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            stream.Seek(1024, SeekOrigin.Begin);
            for (int i = 0; i < result.Length; i++)
            {
                stream.Read(result, i, 1);
            }
            stream.Close();
            Buffer.BlockCopy(result, 0, fatTable, 0,result.Length);
        }
        static  public void PrintFatTable()
        {
            Console.WriteLine("Indx" + "/" + "Val");
            for (int i = 0; i < fatTable.Length; i++)
            {
                Console.WriteLine(" "+i +  " / " + fatTable[i]);
            }
        }
        // بتقولي اول بلوك فاضي ايه الاندكس بتاعه
        static  public int Available_Block_In_FatTable()
        {
            for (int i = 5; i < fatTable.Length; i++)
            {
                if(fatTable[i]==0)
                {
                    return i;
                }
            }
            return -1;
        }
        //بتقولي الاندكس الفلاني ايه الفاليو اللي فيه
        static public int Get_Next(int index)
        {
            if (index > 4)
            {
                return fatTable[index];
            }
            return -1;
        }
        // بديها فاليو واقولها تحطها في الاندكس الفلاني
        static  public void Set_Next(int index,int Value)
        {    
           fatTable[index] = Value;         
        }
        // بترجعلي العدد الفعلي لكل ابلوكات الفاضيه في الفات تابل
        static public int getAvailableBlocks()
        {
            int AvalibleBlocks = 0;
            for (int i = 5; i < 1024; i++)
            {
                if (fatTable[i] == 0)
                    AvalibleBlocks++;
            }
            return AvalibleBlocks;
        }
        static public int getFreeSpace()
        {
            return getAvailableBlocks() * 1024;
        }
    }
}
