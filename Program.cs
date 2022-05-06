using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Shell
{
    class Program
    {

        // لو الامر موجود هترجعلي الرقم الخاص بيه حيث كل امر وله اي دي معين 
        static int Instruction(string instruction)
        {
            switch (instruction)
            {
                case "cd": return 1;
                case "cls": return 2;
                case "dir": return 3;
                case "quit": return 4;
                case "copy": return 5;
                case "del": return 6;
                case "help": return 7;
                case "md": return 8;
                case "rd": return 9;
                case "rename": return 10;
                case "type": return 11;
                case "import": return 12;
                case "export": return 13;
                default: return 0;

            }
        }
        // بتتأكد من صحة الاوامر وبتنده على الفانكشن الخاصة بتنفيذها في حالة كانت صحيحة
        static void Check_Inputs(string Input)
        {
            string[] Inputs = Input.Split(); 
            if (Instruction(Inputs[0]) == 0)
            {
                if (Inputs[0].Length == 0)
                {
                    Console.Write("");
                }
                else
                {
                    Console.WriteLine("  '" + Inputs[0] + "'" + " is not recognized as an internal or external command,operable program or batch file.\n");
                }
            }
            else
            {
                switch (Instruction(Inputs[0]))
                {
                    case 1:

                        ChangeDirectory c = new ChangeDirectory();
                        if (Inputs.Length == 1)
                        {
                            Console.WriteLine(Program.CurrentPath);
                        }
                        else if (Inputs.Length == 2)
                        {
                            Console.WriteLine("\n");
                            c.cd(Inputs[1]);
                        }
                        break;
                    case 2: Console.Clear(); welcome(); break;
                    case 3:
                        Dir d = new Dir();
                        if (Inputs.Length == 1)
                        {
                            d.dir();
                        }
                        else if (Inputs.Length >= 2)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        break;
                    case 4: Environment.Exit(0); break;
                    case 5:
                        Copy co = new Copy();
                        if (Inputs.Length == 1 || Inputs.Length == 2)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        else if (Inputs.Length == 3)
                        {
                            Console.WriteLine("\n");
                            co.copy(Inputs[1], Inputs[2]);
                        }
                        break;
                    case 6:
                        Delete de = new Delete();
                        if (Inputs.Length == 1)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        else if (Inputs.Length == 2)
                        {
                            Console.WriteLine("\n");
                            de.delete(Inputs[1]);
                        }
                        break;
                    case 7:
                        Help h = new Help();
                        if (Inputs.Length == 1)
                        {
                            h.help();
                        }
                        else
                        {
                            if (Instruction(Inputs[1]) == 0)
                            {
                                Console.WriteLine("  '" + Inputs[1] + "'" + " is not recognized as an internal or external command,operable program or batch file.\n");
                            }
                            else
                            {
                                Console.WriteLine("\n");
                                h.Help_Check_Input(Instruction(Inputs[1]));
                            }
                        }
                        break;
                    case 8:
                        MakeDirectory m = new MakeDirectory();
                        if (Inputs.Length == 1)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        else if (Inputs.Length == 2)
                        {
                            Console.WriteLine("\n");
                            m.md(Inputs[1]);
                        }
                        break;
                    case 9:
                        RemoveDirectory r = new RemoveDirectory();
                        if (Inputs.Length == 1)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        else if (Inputs.Length == 2)
                        {
                            Console.WriteLine("\n");
                            r.rd(Inputs[1]);
                        }
                        break;
                    case 10:
                        Rename re = new Rename();
                        if (Inputs.Length == 1 || Inputs.Length == 2)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        else if (Inputs.Length == 3)
                        {
                            Console.WriteLine("\n");
                            re.rename(Inputs[1],Inputs[2]);
                        }
                        break;
                    case 11:
                        Type t = new Type();
                        if (Inputs.Length == 1)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        else if (Inputs.Length == 2)
                        {
                            Console.WriteLine("\n");
                            t.type(Inputs[1]);
                        }
                        break;
                    case 12:
                        Import im = new Import();
                        if (Inputs.Length == 1)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        else if (Inputs.Length == 2)
                        {
                            Console.WriteLine("\n");
                            im.import(Inputs[1]);
                        }
                        break;
                    case 13:
                       Export ex = new Export();
                        if (Inputs.Length == 1 || Inputs.Length == 2)
                        {
                            Console.WriteLine("The syntax of the command is incorrect.");
                        }
                        else if (Inputs.Length == 3)
                        {
                            Console.WriteLine("\n");
                            ex.export(Inputs[1],Inputs[2]);
                        }
                        break;
                }
            }
        }
        // فيها رسالة البداية 
        static void welcome()
        {
            Console.Write("\t\tWelcome To Our Shell \n\t\tMade By Randa Adel & Reham Refaat\n\n");
        }
        static public Directory CurrentDirectory;
        static public string CurrentPath = "";
        static void Main(string[] args)
        {
            CurrentDirectory = Virtual_Disk.Initialize_Virtual_Disk();
            byte[] arr = CurrentDirectory.fileName;
            CurrentPath = "";
            for (int i = 0; i < arr.Length; i++)
            {
                CurrentPath += (char)arr[i];
            }
            welcome();
            while (true)
            {
                Console.Write(CurrentPath);
                Console.Write(">");
                string Input = Console.ReadLine();
                Check_Inputs(Input);
            }
        }
    }
}