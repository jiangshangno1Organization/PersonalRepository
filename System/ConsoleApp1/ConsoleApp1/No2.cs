// <summary>
// Description      :
// Author           : zxq
// Create Time      : 2018/8/1 16:01:15
// Revision history : （序号，修改内容，修改人，修改时间）
// 1、  2018/8/1 16:01:15  Solution
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class No2
    {


      static int  ic = 0;
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

                //Console.WriteLine(bombDir);

                // ic = i;
                //转换 i 进制 string
                string ten = nTOTen(bombDir, i);

                //转为 10 进制
                //string ten = GetTen(cell);
                if (IFPase(ten))
                {
                    Console.WriteLine(i);
                }
            }
        }
        // 014101444
        private static string nTOTen(string data, int j)
        {
            if (ic == 7)
            {
                Console.WriteLine(data);
            }
            string[] dataList = data.Split(' ');

            string value = "";
            foreach (string item in dataList)
            {
                if (item.StartsWith("0"))
                {
                    value += item.Substring(1,1);
                }
                else
                {
                    value += item;
                }
            }
            if (ic == 7)
            {
                Console.WriteLine(value);
            }

            //data = data.Replace(" ", "");
            data = value;
      
            string res = "";
            double da = 0;
            for (int i = 0; i < data.Length; i++)
            {
                int z = data.Length - i - 1;
                int cell = Convert.ToInt32(data.Substring(z, 1));

                //long.Parse(mystr);

                //Console.WriteLine(da + " "+cell + " " + j + "" + i);
                var dad = Math.Pow(j, i) * cell;
                da += dad;
                // da += Convert.ToInt64(cell * Math.Pow(j, i));
                //Console.WriteLine(cell + " " + da);
            }
            res = da.ToString("f1").Replace(".0","");
            //Console.WriteLine(res);
            //Console.WriteLine(res);
            return res;
          
        }


        private static string GetRealData(List<int> data, int i)
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
    
        private static bool IFPase(string number)
        {
            //Console.WriteLine(number);
            if (ic == 7)
            {
                Console.WriteLine(number);
            }
           // Console.WriteLine(number);
            for (int i = 1; i < number.Length + 1; i++)
            {
                //Console.WriteLine(number.Substring(0, i));

                double cell = Convert.ToDouble(number.Substring(0, i));

                bool pa = cell % Convert.ToInt64(i) == 0;
              //  Console.WriteLine(cell + " " + i + " " + pa);
                if (ic == 7)
                {
                   Console.WriteLine(cell + " "  + i + " "+ pa);
                }

                if (!pa)
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

    }
}
