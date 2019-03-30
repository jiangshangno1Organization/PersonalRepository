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
    public class class1
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
                //转换 i 进制 string
                string ten = nTOTen(bombDir, i);

                //转为 10 进制
                if (IFPase(ten))
                {
                    Console.WriteLine(i);
                }
            }
        }
        private static string nTOTen(string data, int j)
        {
            string[] datadd = data.Split(' ');
            decimal dadd = 0;
            for (int i = 0; i < datadd.Length; i++)
            {
                int cell = Convert.ToInt32(datadd[datadd.Length - i - 1]);
                try
                {
                    var dad = Pow(j, i) * cell;
                    dadd += Convert.ToDecimal(dad);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return dadd.ToString();
        }

        private static decimal Pow(decimal x, decimal y)
        {
            decimal dReturn = 1;
            for (int i = 1; i<= y; i++)
            {
                dReturn *= x;
            }
            return dReturn;
        }

        private static bool IFPase(string number)
        {
            for (int i = 1; i < number.Length + 1; i++)
            {
                decimal cell = decimal.Parse(number.Substring(0, i));
                bool pa = cell % Convert.ToDecimal(i) == 0;
                
                if (!pa)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
