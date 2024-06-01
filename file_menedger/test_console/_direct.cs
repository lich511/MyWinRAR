using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace test_console
{
    class _direct:criptor
    {
        public List<_direct> _directs = new List<_direct>();
        public List<_file> _Files = new List<_file>();
        public DirectoryInfo info;
        public void reads (string path)
        {
            int i = 0;
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (var item in dir.GetDirectories())
            {
                _directs.Add(new _direct
                {
                    info = item,
                    name = item.Name
                });
                _directs[i].reads(item.FullName);
                i++;
            }
            FileInfo[] files = dir.GetFiles();
            foreach (var item in files)
            {
                _Files.Add(new _file
                {
                    info = item,
                    Content = File.ReadAllBytes(item.FullName),
                    name = item.Name
                });
            }
        }
        public void write (string path)
        {
            Directory.CreateDirectory(path);
            foreach (var item in _Files)
            {
                File.WriteAllBytes(path + "/" + item.name, item.Content);
            }
            foreach (var item in _directs)
            {
                item.write(path + "/" + item.name);
            }
        }
        public int read_dir()
        {
            int len = 0;
            foreach (var item in _directs)
            {
                len++;
            }
            return len;
        }
        public int read_file()
        {
            int len = 0;
            foreach (var item in _Files)
            {
                len++;
            }
            return len;
        }
        public int read_lenght()
        {
            int len = 0;
            foreach (var item in _directs)
            {
                len += item.read_lenght();
            }
            foreach (var item in _Files)
            {
                len += Convert.ToInt32(item.len);
            }
            return len;
        }
        public void count_line(ref int j,bool criip)
        {
            j += 5;
            foreach (var item in _directs)
            {
                item.count_line(ref j, criip);
            }
            foreach (var item in _Files)
            {
                j += 5;
                if (criip == false)
                {
                    for (int i = 0; i < item.Content.Length; i++)
                    {
                        j++;
                    }
                }
                else
                {
                    for (int i = 0; i < item.cript_content.Length; i++)
                    {
                        j++;
                    }
                }
            }
        }
        public void packing(ref int j,ref string[] lines)
        {
            lines[j] = name;
            lines[j + 1] = _directs.Count.ToString();
            lines[j + 2] = _Files.Count.ToString();
            j += 3;
            foreach (var item in _Files)
            {
                item.packing(ref j, ref lines);
            }
            foreach (var item in _directs)
            {
                item.packing(ref j, ref lines);
            }
        }
        public FileInfo create_temp_file(ref byte[] com,ref int j, string[] lines,string path_dir)
        {
            string name1 = lines[j];
            com = new byte[Convert.ToInt32(lines[j + 1])];
            int leen = Convert.ToInt32(lines[j + 1]);
            j += 3;
            for (int i = 0; i < leen; i++)
            {
                com[i] = Convert.ToByte(lines[j]);
                j++;
            }
            File.WriteAllBytes(path_dir + "/" + name1, com);
            FileInfo info1 = new FileInfo(path_dir + "/" + name1);
            File.Delete(path_dir + "/" + name1);
            return info1;
        }
        public int[] read_cript_file(ref int j, string[] lines)
        {
            string name1 = lines[j];
            int[] com = new int[Convert.ToInt32(lines[j + 1])];
            int leen = Convert.ToInt32(lines[j + 1]);
            j += 3;
            for (int i = 0; i < leen; i++)
            {
                com[i] = Convert.ToInt32(lines[j]);
                j++;
            }
            return com;
        }
        public void unpacking(ref int j,string[] lines,string path_dir,bool criiipt)
        {
            int cd = 0;
            int cf = 0;
            byte[] com = new byte[1];
            name = lines[j];
            cd = Convert.ToInt32(lines[3]);
            cf = Convert.ToInt32(lines[4]);
            j += 3;
            Directory.CreateDirectory(path_dir + "/" + name);
            for (int i = 0; i < cf; i++)
            {
                if (criiipt == false)
                {
                    _Files.Add(new _file
                    {
                        name = lines[j],
                        len = Convert.ToInt32(lines[j + 1]),
                        info = create_temp_file(ref com, ref j, lines, path_dir),
                        Content = com,
                    });
                }
                else
                {
                    _Files.Add(new _file
                    {
                        name = lines[j],
                        cript_len = lines[j + 2],
                        cript_content = read_cript_file(ref j, lines)
                    });
                }
            }
            for (int i = 0; i < cd; i++)
            {
                _directs.Add(new _direct());
                _directs[i].unpacking(ref j, lines, path_dir, criiipt);
            }
            Directory.Delete(path_dir + "/" + name, true);
        }
        public void cript_direct(string pas)
        {
            foreach (var item in _Files)
            {
                int leen = 0;
                item.cript_content = c_cripts(pas, item.Content,ref leen);
                item.cript_len = Convert.ToString(leen);
                item.Content = new byte[1];
            }
            foreach (var item in _directs)
            {
                item.cript_direct(pas);
            }
        }
        public void decript_direct(string pas,ref bool right)
        {
            byte[] result;
            do
            {
                foreach (var item in _Files)
                {
                    result = c_decripts(pas, item.cript_content,ref right, item.cript_len);
                    if (right == true)
                    {
                        item.Content = result;
                        item.cript_content = new int[1];
                    }
                    else break;
                }
                if (right == false) break;
                foreach (var item in _directs)
                {
                    item.decript_direct(pas, ref right);
                }
                break;
            } while (true);
        }
        public string name { get; set; }
    }
}
