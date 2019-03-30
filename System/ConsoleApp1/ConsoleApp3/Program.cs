using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//引用Microsoft.Office.Interop.Excel.dll文件 
//添加using
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ConsoleApp3
{
    public class ExcelHelp
    {
        static void Main(string[] args)
        {
            //创建Application
            Excel.Application excelApp = new Excel.Application();

            object missing = System.Reflection.Missing.Value;
            //创建Excel对象
            Excel.Workbook wb = excelApp.Workbooks.Open("C:\\Users\\zxq\\Documents\\WeChat Files\\zhuxiaoqiang94\\Files\\九月份考勤台账本.xlsx", missing, false, missing, missing, missing,
            missing, missing, missing, true, missing, missing, missing, missing, missing);
            //取得第一个工作薄
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets["后勤"];

            //***获取最后一行、一列的两种方法 * **
            //获取已用的范围数据
            int rowsCount = ws.UsedRange.Rows.Count;
            int colsCount = ws.UsedRange.Columns.Count;
            rowsCount = ws.get_Range("A65536", "A65536").get_End(Microsoft.Office.Interop.Excel.XlDirection.xlUp).Row;
            colsCount = 60;//ws.get_Range("ZZ1", "ZZ1").get_End(Microsoft.Office.Interop.Excel.XlDirection.xlToLeft).Column;

            //获取各列
            //***将Excel数据存入二维数组 * **
            //rowsCount：最大行    colsCount：最大列
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[rowsCount, colsCount];
            Range rng = (Microsoft.Office.Interop.Excel.Range)ws.get_Range(c1, c2);
            object[,] exceldata = (object[,])rng.get_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault);

            //获取第二列 所有title  用于记录 需填充 获取
            Dictionary<string, int> allTitle = new Dictionary<string, int>();
            List<string> needKeepFile = new List<string>() ;
            needKeepFile.Add("姓名");
            needKeepFile.Add("出勤天数");
            needKeepFile.Add("出差");
            needKeepFile.Add("年休");
            needKeepFile.Add("例会天数");
            needKeepFile.Add("晚加班");
            needKeepFile.Add("可享受午餐数");
            needKeepFile.Add("实际午餐数");
            needKeepFile.Add("可享受晚餐数");
            needKeepFile.Add("实际晚餐数");
            needKeepFile.Add("多刷工作餐次数");

            //初始化需求列名
            for (int j = 1; j < colsCount+1; j++)
            {
                string cellTitle = Convert.ToString(exceldata[2, j]);
                if (needKeepFile.Contains(cellTitle))
                {
                    allTitle.Add(cellTitle ,j);
                }
            }

            //遍历所有行 （填充）
            for (int i = 1; i < rowsCount+1; i++)
            {
                if (i > 2)
                {
                    string name = Convert.ToString(exceldata[i, allTitle["姓名"]]);
                    //根据已有数据 赋值 计算
                    if (name.Equals("朱小强"))
                    {
                        ws.Cells[i, allTitle["出勤天数"]] = 100;
                    }
                }
            }

            //int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
            //int columnsint = ws.UsedRange.Cells.Columns.Count;//得到列数
            //Excel.Range rng1 = ws.Rows["1:3", Type.Missing]; //item
            //object[,] arr = (object[,])rng1.Value2;
            //double[] arrSO2 = new double[columnsint - 1];
            //double[] arrNox = new double[columnsint - 1];
            //double[] arrCO = new double[columnsint - 1];
            string str = "";
            //for (int i = 0; i < columnsint - 1; i++)
            //{
            //    arrSO2[i] = Convert.ToDouble(arr[1, i + 2]);
            //    arrNox[i] = Convert.ToDouble(arr[2, i + 2]);
            //    arrCO[i] = Convert.ToDouble(arr[3, i + 2]);
            //}
            //ws.Cells[4, 7] = "SO2浓度标准偏差：";
            //ws.Cells[4, 8] = "0.1111651";
            //ws.Cells[5, 7] = "NOX浓度标准偏差：";
            //ws.Cells[5, 8] = "0.2222152";

            wb.Save();
            excelApp.Quit();
            wb = null;
            ws = null;
            KeyMyExcelProcess.Kill(excelApp);
            GC.Collect();
        }

        /// <summary>
        /// 关闭Excel进程
        /// </summary>
        public class KeyMyExcelProcess
        {
            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
            public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
            {
                try
                {
                    IntPtr t = new IntPtr(excel.Hwnd);   //得到这个句柄，具体作用是得到这块内存入口
                    int k = 0;
                    GetWindowThreadProcessId(t, out k);   //得到本进程唯一标志k
                    System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //得到对进程k的引用
                    p.Kill();     //关闭进程k
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        public class Enployee
        {
            public string d { get; set; }

            public string i { get; set; }
        }

    }
}

