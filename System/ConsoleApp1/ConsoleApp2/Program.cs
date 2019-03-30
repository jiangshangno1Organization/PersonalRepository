using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft;
using Newtonsoft.Json;

namespace ConsoleApp2
{
    class Program
    {

        public class p1
        {
           public string q1 { get; set; }             
            public string q2 { get; set; }
        }

        public class p2
        {
            public string q1 { get; set; }
        }

        static void Main(string[] args)
        {

            p1 p1 = new p1()
            {
                q1 = "qq",
                q2 = "ww"
            };

            string data =JsonConvert.SerializeObject(p1);

            p2 da = JsonConvert.DeserializeObject<p2>(data);


            //string da = "http://192.168.0.20:51099/api/dt/";
            //int ooo = 1;

            //DataTable dataTable = new DataTable();

            //dataTable.Columns.Add("ddd");

            //DataRow dr = dataTable.NewRow();
            //dr["ddd"] = "dsa";
            //dataTable.Rows.Add(dr);

            //string sf = dr["ddd"] + "|";

            //int sfd = ddd();

            //DateTime dt = Convert.ToDateTime("2018-02-01");
            //dt = dt.AddDays(-1);
            //Console.WriteLine(ooo.ToString().PadLeft(3,'0'));

            //Dictionary<string, string> dictionary = new Dictionary<string, string>();
            //dictionary.Add("123","123");

            //string sfs = dictionary["123"];
            //string fddg = dictionary["555"];
            //string fddgsd = dictionary[null];

            //Console.WriteLine(sf);
            
            Console.ReadLine();
        }

        public static int ddd()
        {
            int t = 0;
            try
            {
                t = 1;
                return t;

            }
            catch (Exception ex)
            {
                t = 2;
            }
            finally
            {
                t = 3;
            }
            t = 5;
            return t;


        }

    }



}

class sdss
{
    public int a { get; set; }
    public int b { get; set; }

}
