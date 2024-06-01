using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_console
{
    class criptor
    {
        public int[] c_cripts(ref string password,byte[] content)
        {
            int[] cript_content = new int[content.Length];
            int len = 0;
            for (int i = 0; i < content.Length; i++)
            {
                cript_content[i] = content[i];
                len += Convert.ToString(content[i]).Length;
            }
            for (int i = 0; i < password.Length; i++)
            {
                for (int j = 0; j < content.Length; j++)
                {
                    cript_content[j] = cript_content[j] * Convert.ToInt32(password[i]);
                }
            }
            password = Convert.ToString(password.Length) + password + Convert.ToString(len);
            return cript_content;
        }
        public int[] c_cripts(string password, byte[] content,ref int len)
        {
            int[] cript_content = new int[content.Length];
            len = 0;
            for (int i = 0; i < content.Length; i++)
            {
                cript_content[i] = content[i];
                len += Convert.ToString(content[i]).Length;
            }
            for (int i = 0; i < password.Length; i++)
            {
                for (int j = 0; j < content.Length; j++)
                {
                    cript_content[j] = cript_content[j] * Convert.ToInt32(password[i]);
                }
            }
            password = Convert.ToString(password.Length) + password + Convert.ToString(len);
            return cript_content;
        }
        public byte[] c_decripts(string password,int[] cript_content)
        {
            int pos = Convert.ToInt32(password[0].ToString());
            string pas = password.Substring(1, pos);
            string len = password.Substring(pos + 1, password.Length - pos - 1);
            for (int i = 0; i < pas.Length; i++)
            {
                for (int j = 0; j < cript_content.Length; j++)
                {
                    cript_content[j] = cript_content[j] / Convert.ToInt32(pas[i]);
                }
            }
            int len_cript = 0;
            for (int i = 0; i < cript_content.Length; i++)
            {
                len_cript += cript_content[i].ToString().Length;
            }
            if (len_cript.ToString() == len)
            {
                Console.WriteLine("ключ правильный");
                Console.ReadKey();
                byte[] content = new byte[cript_content.Length];
                for (int i = 0; i < cript_content.Length; i++)
                {
                    content[i] = Convert.ToByte(cript_content[i]);
                }
                return content;
            }
            else
            {
                Console.WriteLine("ключ неправильный");
                Console.ReadKey();
                return new byte[1];
            }
        }
        public byte[] c_decripts(string password, int[] cript_content,ref bool right,string cript_len)
        {
            string pas = password;
            string len = cript_len;
            for (int i = 0; i < pas.Length; i++)
            {
                for (int j = 0; j < cript_content.Length; j++)
                {
                    cript_content[j] = cript_content[j] / Convert.ToInt32(pas[i]);
                }
            }
            int len_cript = 0;
            for (int i = 0; i < cript_content.Length; i++)
            {
                len_cript += cript_content[i].ToString().Length;
            }
            if (len_cript.ToString() == len)
            {
                right = true;
                byte[] content = new byte[cript_content.Length];
                for (int i = 0; i < cript_content.Length; i++)
                {
                    content[i] = Convert.ToByte(cript_content[i]);
                }
                return content;
            }
            else
            {
                right = false;
                return new byte[1];
            }
        }
    }
}
