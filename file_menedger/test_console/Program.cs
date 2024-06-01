using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace test_console
{
    class Program
    {
        static List<_cell> cells = new List<_cell>(10);
        static string path_program = Directory.GetCurrentDirectory();
        static void Main(string[] args)
        {
            int temp_int = 13;
            string temp_str;
            for (int i = 0; i < 10; i++)
            {
                cells.Add(new _cell { isNull = true, cript = false });
            }
            do
            {
                write_info_all();
                try
                {
                    temp_str = Console.ReadLine();
                    try
                    {
                        temp_int = Convert.ToInt32(temp_str);
                    }
                    catch
                    {
                        if (temp_str == "path")
                        {
                            Console.WriteLine("введите путь");
                            string a = Console.ReadLine();
                            if (a == "def") a = Directory.GetCurrentDirectory();
                            if (Directory.Exists(a) == true) path_program = a;
                        }
                        if (temp_str == "seatch")
                        {
                            Console.WriteLine("введите путь");
                            string a = Console.ReadLine();
                            if (Directory.Exists(a) == true) seatch(a);
                        }
                    }
                   
                    if (temp_int < 11 && temp_int > 0)
                        use_cell(temp_int - 1);

                }
                catch(Exception ex) { Console.WriteLine(ex); Console.ReadKey(); }
                Console.Clear();
            } while (true);
        }
        static void write_info_all()
        {
            Console.WriteLine("path=" + path_program);
            Console.WriteLine("path-поменять путь к папке сохранения");
            Console.WriteLine("seatch-поиск запакованых файлов");
            Console.WriteLine("--------------------");
            for (int i = 0; i < 10; i++)
            {
                if (cells[i].isNull == true) Console.WriteLine($"{i + 1})null");
                else
                    if (cells[i].isFile == true) Console.WriteLine($"{i + 1}){cells[i]._File.name}");
                else Console.WriteLine($"{i+1}){cells[i]._Direct.name}");
            }
            Console.WriteLine("--------------------");
        }
        static void write_info(int i)
        {
            if(cells[i].isFile == true)
            {
                Console.WriteLine($"имя:{cells[i]._File.name}");
                if (cells[i].cript == false)
                    Console.WriteLine($"размер:{cells[i]._File.len}byte");
                Console.WriteLine($"зашифрован:{cells[i].cript}");
                Console.WriteLine("-------------------------");
            }
            else
            {
                Console.WriteLine($"имя:{cells[i]._Direct.name}");
                Console.WriteLine($"кол-во папок:{cells[i]._Direct.read_dir()}");
                Console.WriteLine($"кол-во файлов:{cells[i]._Direct.read_file()}");
                Console.WriteLine($"зашифрован:{cells[i].cript}");
                Console.WriteLine("-------------------------");
            }
        }
        static void use_cell(int i)
        {
            if (cells[i].isNull == true) write_file(i);
            else
            {
                string temp_str;
                do
                {
                    Console.Clear();
                    write_info(i);
                    Console.WriteLine($"del-удалить\nexit-выход\nsave-запаковать");
                    if (cells[i].cript == false)
                        Console.WriteLine("export-распаковать");
                    if (cells[i].cript == false)
                        Console.WriteLine("cript-зашифровать");
                    else
                        Console.WriteLine("decript-расшифровать");
                    temp_str = Console.ReadLine();
                    switch (temp_str)
                    {
                        case "del":
                            cells[i] = new _cell { isNull = true, cript = false };
                            temp_str = "exit";
                            break;
                        case "export": if(cells[i].cript == false)cells[i].write_cell(path_program); break;
                        case "save": cells[i].save_cell(path_program);  break;
                        case "cript":if (cells[i].cript == false)cells[i].cript_(); break;
                        case "decript": if (cells[i].cript == true) cells[i].decript(); break;
                        case "1": break;
                    }
                    if (temp_str == "exit") break;
                } while (true);
            }
        }
        static void write_file(int i)
        {
            Console.WriteLine("введи адресс");
            string path = Console.ReadLine();
            if (File.Exists(path) == true)
            {
                FileInfo info = new FileInfo(path);
                if(info.Extension != ".cel")
                    cells[i].add_file(path);
            }
            if(Directory.Exists(path) == true)
            {
                cells[i].add_direct(path);
                cells[i]._Direct.reads(path);
            }
        }
        static void seatch(string path_)
        {
            Console.WriteLine("начинаю поиск");
            DirectoryInfo info_d = new DirectoryInfo(path_);
            foreach (var item in info_d.GetFiles())
            {
                if(item.Extension == ".cel")
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (cells[i].isNull == true)
                        {
                            cells[i].read_cell(path_program, item.FullName);
                            break;
                        }
                    }
                }
            }
        }
    }
}
