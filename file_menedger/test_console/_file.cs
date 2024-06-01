using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace test_console
{
    class _file
    {
        public byte[] Content { get; set; }
        public int[] cript_content { get; set; }
        public string name { get; set; }
        public int len { get; set; }
        public string cript_len { get; set; }
        public FileInfo info;
        public void packing(ref int j,ref string[] lines)
        {
            lines[j] = name;
            lines[j + 2] = cript_len;
            if (lines[0] == "False")
            {
                lines[j + 1] = Content.Length.ToString();
                j += 3;
                for (int i = 0; i < Content.Length; i++)
                {
                    lines[j] = Content[i].ToString();
                    j++;
                }
            }
            else
            {
                lines[j + 1] = cript_content.Length.ToString();
                j += 3;
                for (int i = 0; i < cript_content.Length; i++)
                {
                    lines[j] = cript_content[i].ToString();
                    j++;
                }
            }
        }
    }
}
