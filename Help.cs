using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class Help
    {
        public void help()
        {
            Console.WriteLine("cd\t" + "Change the current default directory to . If the argument is not present, report the current directory. If the directory does not exist an appropriate error should be reported.");
            Console.WriteLine("cls\t" + "Clear the screen.");
            Console.WriteLine("dir\t" + "List the contents of directory .");
            Console.WriteLine("quit\t" + "Quit the shell.");
            Console.WriteLine("copy\t" + "Copies one or more files to another location .");
            Console.WriteLine("del\t" + "Deletes one or more files.");
            Console.WriteLine("help\t" + "Provides Help information for commands.");
            Console.WriteLine("md\t" + "Creates a directory.");
            Console.WriteLine("rd\t" + "Removes a directory.");
            Console.WriteLine("rename\t" + "Renames a file.");
            Console.WriteLine("type\t" + "Displays the contents of a text file.");
            Console.WriteLine("import\t" + "Import text file(s) from your computer.");
            Console.WriteLine("export\t" + "Export text file(s) to your computer.");
        }

        //فانكشن بتتأكد من وجود الاوامر مع بعت باراميتر ليها باسم الامر 
        public void Help_Check_Input(int Input)
        {
            switch (Input)
            {
                case 1:
                    Console.WriteLine("Change the current default directory to . If the argument is not present, report the current directory. If the directory does not exist an appropriate error should be reported.");
                    Console.WriteLine("\nCD\n");
                    break;
                case 2:
                    Console.WriteLine("Clear the screen.");
                    Console.WriteLine("\nCLS\n");
                    break;
                case 3:
                    Console.WriteLine("List the contents of directory .");
                    Console.WriteLine("\nDIR\n");
                    break;
                case 4:
                    Console.WriteLine("Quit the shell.");
                    Console.WriteLine("\nQUIT\n");
                    break;
                case 5:
                    Console.WriteLine("Copies one or more files to another location .");
                    Console.WriteLine("\nCOPY\n");
                    break;
                case 6:
                    Console.WriteLine("Deletes one or more files.");
                    Console.WriteLine("\nDEL\n");
                    break;
                case 7:
                    Console.WriteLine("Provides Help information for commands.");
                    Console.WriteLine("\nHELP\n");
                    break;
                case 8:
                    Console.WriteLine("Creates a directory.");
                    Console.WriteLine("\nMD\n");
                    break;
                case 9:
                    Console.WriteLine("Removes a directory.");
                    Console.WriteLine("\nRD\n");
                    break;
                case 10:
                    Console.WriteLine("Renames a file.");
                    Console.WriteLine("\nRENAME\n");
                    break;
                case 11:
                    Console.WriteLine("Displays the contents of a text file.");
                    Console.WriteLine("\nTYPE\n");
                    break;
                case 12:
                    Console.WriteLine("Import text file(s) from your computer.");
                    Console.WriteLine("\nIMPORT\n");
                    break;
                case 13:
                    Console.WriteLine("Export text file(s) to your computer.");
                    Console.WriteLine("\nEXPORT\n");
                    break;

            }
        }
    }
}
