using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace test_console
{
    class _cell:criptor
    {
        public _direct _Direct;
        public _file _File;
        public void add_file(string path)
        {
            _File = new _file
            {
                Content = File.ReadAllBytes(path),
                info = new FileInfo(path),
            };
            _File.name = _File.info.Name;
            _File.len = Convert.ToInt32(_File.info.Length);
            isNull = false;
            isFile = true;
        }
        public void add_direct(string path)
        {
            _Direct = new _direct
            {
                info = new DirectoryInfo(path)
                //авто добавление папок
            };
            _Direct.name = _Direct.info.Name;
            isNull = false;
            isFile = false;
        }
        public void write_cell(string path_)
        {
            if (isFile == true)
            {
                if (cript == false)
                    File.WriteAllBytes(path_ + "/" + _File.name, _File.Content);
            }
            else _Direct.write(path_ + "/" + _Direct.name);
        }
        public void read_cell(string path_direct,string path_)
        {
            int j = 0;
            string[] lines = File.ReadAllLines(path_);
            if(lines[1] == "True")
            {
                if (lines[2] == "False")
                {
                    byte[] con = new byte[Convert.ToInt32(lines[3])];
                    for (int i = 0; i < Convert.ToInt32(lines[3]); i++)
                    {
                        con[i] = Convert.ToByte(lines[i + 4]);
                    }
                    File.WriteAllBytes(path_direct +"/"+ lines[0], con);
                    add_file(path_direct + "/" + lines[0]);
                    File.Delete(path_direct + "/" + lines[0]);
                    j++;
                }
                else
                {
                    isFile = true;
                    cript = true;
                    isNull = false;
                    int[] con = new int[Convert.ToInt32(lines[3])];
                    for (int i = 0; i < Convert.ToInt32(lines[3]); i++)
                    {
                        con[i] = Convert.ToInt32(lines[i + 5]);
                    }
                    _File = new _file
                    {
                        name = lines[0],
                        cript_content = con
                    };
                }
            }
            else
            {
                if(lines[0] == "False")
                {
                    cript = false;
                    isFile = false;
                    isNull = false;
                    j += 2;
                    _Direct = new _direct();
                    _Direct.unpacking(ref j, lines, path_direct, false);
                }
                else
                {
                    //зашифрованая папка
                    cript = true;
                    isFile = false;
                    isNull = false;
                    j += 2;
                    _Direct = new _direct();
                    _Direct.unpacking(ref j, lines, path_direct, true);
                }
            }
        }
        public void save_cell(string path_program)
        {
            if(isFile == true)
            {
                string[] lines;
                if (cript == false)
                    lines = new string[5 + _File.Content.Length];
                else
                    lines = new string[5 + _File.cript_content.Length];
                int j = 5;
                lines[0] = _File.info.Name;
                lines[1] = isFile.ToString();
                lines[2] = cript.ToString();
                lines[3] = _File.Content.Length.ToString();
                lines[4] = _File.cript_len;
                if (cript == false)
                    for (int i = 0; i < _File.Content.Length; i++)
                    {
                        lines[j] = _File.Content[i].ToString();
                        j++;
                    }
                else
                    for (int i = 0; i < _File.cript_content.Length; i++)
                    {
                        lines[j] = _File.cript_content[i].ToString();
                        j++;
                    }
                File.WriteAllLines(path_program + "/" + _File.info.Name + ".cel", lines);
            }
            else
            {
                int j = 5;
                if (cript == false)
                    _Direct.count_line(ref j, false);
                else
                    _Direct.count_line(ref j, true);
                string[] lines = new string[j];
                j = 2;
                lines[0] = cript.ToString();
                lines[1] = isFile.ToString();
                _Direct.packing(ref j, ref lines);
                File.WriteAllLines(path_program + "/" + _Direct.name + ".cel", lines);
            }
        }
        public void cript_()
        {
            string pas;
            do
            {
                Console.WriteLine("укажите ключ шифрования");
                pas = Console.ReadLine();
                try
                {
                    Convert.ToInt32(pas);
                    break;
                }
                catch { }
            } while (true);
            Console.WriteLine("запускаю шифрование");
            if (isFile == true)
            {
                _File.cript_content = c_cripts(ref pas, _File.Content);
                _File.Content = new byte[_File.Content.Length];
            }
            else
            {
                _Direct.cript_direct(pas);
            }
            cript = true;
            Console.WriteLine("успешно\nключ дешифровки:" + pas);
            Console.ReadKey();
        }
        public void decript()
        {
            Console.WriteLine("укажите ключ дешифровки");
            string pas;
            do
            {
                try
                {
                    pas = Console.ReadLine();
                    Convert.ToInt32(pas);
                    break;
                }
                catch 
                {
                    Console.WriteLine("формат пароля неправильный");
                }
            } while (true);
            byte[] resulte;
            if (isFile == true)
            {
                resulte = c_decripts(pas, _File.cript_content);
                if (resulte != new byte[1])
                {
                    _File.Content = resulte;
                    _File.cript_content = new int[1];
                    cript = false;
                }
            }
            else
            {
                bool right = true;
                _Direct.decript_direct(pas,ref right);
                if(right == true)
                {
                    cript = false;
                    Console.WriteLine("успешно");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("ошибка , неверный ключ");
                    Console.ReadKey();
                }
            }
        }
        public bool isFile { get; set; }
        public bool isNull { get; set; }
        public bool cript { get; set; }
    }
}
