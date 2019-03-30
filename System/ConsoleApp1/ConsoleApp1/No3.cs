using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    public class Number
    {
        public string Characters
        {
            get;
            set;
        }

        public int Length
        {
            get
            {
                if (Characters != null)
                    return Characters.Length;
                else
                    return 0;
            }

        }

        public Number()
        {
            Characters = "0123456789";
        }

        public Number(string characters)
        {
            Characters = characters;
        }

        /// <summary>
        /// 数字转换为指定的进制形式字符串
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string ToString(long number)
        {
            List<string> result = new List<string>();
            long t = number;

            while (t > 0)
            {
                var mod = t % Length;
                t = Math.Abs(t / Length);
                var character = Characters[Convert.ToInt32(mod)].ToString();
                result.Insert(0, character);
            }

            return string.Join("", result.ToArray());
        }

        /// <summary>
        /// 指定字符串转换为指定进制的数字形式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public long FromString(string str)
        {
            long result = 0;
            int j = 0;
            foreach (var ch in new string(str.ToCharArray().Reverse().ToArray()))
            {
                if (Characters.Contains(ch))
                {
                    result += Characters.IndexOf(ch) * ((long)Math.Pow(Length, j));
                    j++;
                }
            }
            return result;
        }

    }



    class Program
    {
        static void Print(long number, Number adapter)
        {
            Console.WriteLine("输入数字：{0}", number);
            Console.WriteLine("规则：{0}\t\t进制：{1}进制", adapter.Characters, adapter.Length);
            var numtostr = adapter.ToString(number);
            Console.WriteLine("转换结果：{0}", numtostr);
            var strtonum = adapter.FromString(numtostr);
            Console.WriteLine("逆向转换结果：{0}", strtonum);
            Console.WriteLine();
            Console.WriteLine("============ 无聊的分割线 ============");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            //传统的2进制
            Number n1 = new Number("01");
            //传统的8进制
            Number n2 = new Number("01234567");
            //传统的16进制
            Number n3 = new Number("0123456789ABCDEF");
            //自定义编码的N进制，这个可以用来做验证码？
            Number n4 = new Number("爹妈说名字太长躲在树后面会被部落发现");
            //山寨一个短网址
            Number n5 = new Number("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

            Print(65535, n1);
            Print(65535, n2);
            Print(65535, n3);
            Print(65535, n4);
            Print(165535, n5);

            Console.ReadKey();

        }
    }
}
