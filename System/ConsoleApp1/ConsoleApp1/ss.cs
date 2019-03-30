// <summary>
// Description      :
// Author           : zxq
// Create Time      : 2018/8/1 16:01:15
// Revision history : （序号，修改内容，修改人，修改时间）
// 1、  2018/8/1 16:01:15
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ss
    {
        static void Main(string[] args)
        {
            string bombDir = Console.ReadLine();

            // 01 02 03
            string[] data = bombDir.Split(' ');
            List<string> list = data.ToList();
            List<int> dataList = list.Select(item => Convert.ToInt32(item)).ToList();

            int min = dataList.Max() + 1;
            int max = 36;


            for (int i = min; i < max + 1; i++)
            {
                //转换 i 进制
                string cell = GetRealData(dataList, min);
                //转为 10 进制
                string ten = GetTen(cell);
                if (IFPase(ten))
                {
                    Console.WriteLine(i);
                }
            }



        }

        private static string GetRealData(List<int> data, int jz)
        {
            string res = string.Empty;
            foreach (int item in data)
            {
                if (item >= 10)
                {
                    // 10 
                    // 97 a  
                    res += (Char)(item + 87);
                }
                else
                {
                    res += item.ToString();
                }

            }
            return res;
        }

        private static string Convertto(long n, int jinzhi)
        {
            string res = string.Empty;
            string r = "";
            string[] map = { "A", "B", "C", "D", "E", "F" };
            long t = Math.Abs(n);
            int ysh;
            while (0 != t)
            {
                ysh = (int)(t % jinzhi);
                if (ysh >= 10 && ysh <= 15)
                {
                    r = map[ysh - 10] + r;
                }
                else
                {
                    r = Convert.ToString(ysh) + r;
                }
                t = t / jinzhi;
            }
            res = string.Format("{0}{1}", (n > 0 ? "" : "-"), r);
            return res;
        }

        private static bool IFPase(string number)
        {
            for (int i = 1; i < number.Length; i++)
            {
                int cell = Convert.ToInt32(number.Substring(0, i));
                bool b = cell % (i + 1) == 0;
                if (!b)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isExists(string str)
        {
            return Regex.Matches(str, "[a-zA-Z]").Count > 0;
        }

        public static string GetTen(string data)
        {
            string res = "";
            for (int i = 0; i < data.Length; i++)
            {
                string cell = data.Substring((i - 1) < 0 ? 0 : (i - 1), i);
                if (isExists(cell))
                {
                    cell += (char)(Convert.ToInt32(cell) + 87);
                }
                else
                {
                    res += cell;
                }
            }
            return res;
        }

        /// <summary>
        /// 转为10进制
        /// </summary>
        /// <param name="v"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
        public string ConvertToString(int v, int fromBase)
        {
            if (fromBase < 2 || fromBase > 36) throw new ArgumentException();
            List<char> cs = new List<char>(36);
            while (v > 0)
            {
                int x = v % fromBase;
                int c = 48;
                if (x > 10) { c = 87; }
                cs.Add((char)(x + c));
                v /= fromBase;
            }
            cs.Reverse();
            return new string(cs.ToArray());
        }
    }
}
