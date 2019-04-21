using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
namespace ConsoleApp4
{
    class Program
    {
        public object StringBuffer { get; private set; }

        public static void aa()
        {
            int a = 1;
        }

        class dd
        {
            public string id { get; set; }
            public int a { get; set; }
            public string df { get; set; }

            public override string ToString()
            {
                return $"{id}  {a} {df}";
            }
        }

        #region MyRegion

       
        public interface BaseStructure
        {
            string GetTitle();

            string ToString();
        }

        /// <summary>
        /// 电子秤商品数据格式
        /// </summary>
        public class BaseGoodsStructure : BaseStructure
        {
            /// <summary>
            /// 生鲜码
            /// </summary>
            private int iD;

            /// <summary>
            /// 部门ID
            /// </summary>
            private int departmentID = 0;

            /// <summary>
            /// 组ID
            /// </summary>
            private int groupID = 1;

            /// <summary>
            /// 
            /// </summary>
            private string itemCode;

            /// <summary>
            /// 名称1
            /// </summary>
            private string name1;

            /// <summary>
            /// 名称2
            /// </summary>
            private string name2;

            /// <summary>
            /// 名称3
            /// </summary>
            private string name3;

            /// <summary>
            /// 标签
            /// </summary>
            private string label1ID = "2";

            /// <summary>
            /// sLabel2ID
            /// </summary>
            private string label2ID = "0";

            /// <summary>
            /// 条码1类型
            /// </summary>
            private string barcodeType1 = "0";

            /// <summary>
            /// 条码二类型
            /// </summary>
            private string barcodeType2 = "152";

            /// <summary>
            /// 称重单位编号 kg
            /// </summary>
            private string unitID = "4";

            /// <summary>
            /// 单价，最多小数点后两位,最大为 999999.99
            /// </summary>
            private string price;

            /// <summary>
            /// 保鲜天（小时）数	
            /// </summary>
            private string freshnessDate = "0";

            /// <summary>
            /// 包装误差。0-99%	
            /// </summary>
            private string packageRange = "0";

            /// <summary>
            /// 包装类型 0：正常 1：定重 2：定价 3：定重定价 4：拼盘
            /// </summary>
            private string packageType = "0";

            /// <summary>
            /// 包装重量，或者现在限售重量	
            /// </summary>
            private string packageWeight;

            /// <summary>
            /// 备注一
            /// </summary>
            private string text1ID;

            /// <summary>
            /// 备注二
            /// </summary>
            private string text2ID;

            /// <summary>
            /// 备注三
            /// </summary>
            private string text3ID;

            /// <summary>
            /// 备注四
            /// </summary>
            private string text4ID;

            /// <summary>
            /// 折扣 0:无折扣
            /// </summary>
            private string discountID = "0";

            /// <summary>
            /// sFlag1  控制 商品附加属性 是否打印
            /// </summary>
            private string flag1 = "1";

            /// <summary>
            /// sFlag2
            /// </summary>
            private string flag2 = "0";

            /// <summary>
            /// 皮重
            /// </summary>
            private string tareID = "0";

            /// <summary>
            /// sICEValue
            /// </summary>
            private string iCEValue = "0";

            /// <summary>
            /// 生产日期 0 ：系统日期  1：【ProducedDate】 例：1899/12/30 0:00:01
            /// </summary>
            private int producedDate = 0;

            /// <summary>
            /// 包装天数  0..999
            /// </summary>
            private int packageDays = 0;

            /// <summary>
            /// 包装小时数 0..99
            /// </summary>
            private int packageHours = 0;

            /// <summary>
            /// sFlag3
            /// </summary>
            private string flag3 = "0";

            /// <summary>
            /// sOriginID
            /// </summary>
            private string originID = "0";

            /// <summary>
            /// 折扣相关
            /// </summary>
            private string discountRate = "0";

            /// <summary>
            /// 皮重值
            /// </summary>
            private string tareValue = "0";

            /// <summary>
            /// sHalfDiscount
            /// </summary>
            private string halfDiscount = "0";

            /// <summary>
            /// sQuarterDiscount
            /// </summary>
            private string quarterDiscount = "0";

            /// <summary>
            /// sTax1
            /// </summary>
            private string tax1 = "0";

            /// <summary>
            /// sTax2
            /// </summary>
            private string tax2 = "0";

            /// <summary>
            /// 保质期  0.。99
            /// </summary>
            private int validDate = 0;

            /// <summary>
            /// sText5ID
            /// </summary>
            private string text5ID = "0";

            /// <summary>
            /// sText6ID
            /// </summary>
            private string text6ID = "0";

            /// <summary>
            /// sText7ID
            /// </summary>
            private string text7ID = "0";

            /// <summary>
            /// sText8ID
            /// </summary>
            private string text8ID = "0";

            /// <summary>
            /// 最高单价
            /// </summary>
            private string limitPrice = "0";

            /// <summary>
            /// sTraceabilityCode
            /// </summary>
            private string traceabilityCode = "0";

            /// <summary>
            /// sPackagePrice
            /// </summary>
            private string packagePrice = "0";

            /// <summary>
            /// 生产日期定义规则 0：系统日期 1：【ProduceDate】
            /// </summary>
            private string producedDateRule = "0";

            /// <summary>
            /// 包装日期定义规则 0：系统日期 1：【ProduceDate】
            /// </summary>
            private string packageDateFrom = "0";

            /// <summary>
            /// 保鲜期的基准时间  0：系统日期 + freshnessDate  1：生产日期 + freshnessDate 2：包装日期 + freshnessDate
            /// </summary>
            private string freshnessDateFrom = "0";

            /// <summary>
            /// 保质期的基准时间 0：系统日期 + validDate 1：生产日期 + validDate 2:包装日期 + validDate
            /// </summary>
            private string validDateFrom = "0";

            /// <summary>
            /// sDiscountBeginTime ?? 1899/12/30 0:00:01
            /// </summary>
            private string discountBeginTime = "";

            /// <summary>
            /// sDiscountEndTime  ?? 1899/12/30 0:00:01
            /// </summary>
            private string discountEndTime = "";

            /// <summary>
            /// sDiscountPrice
            /// </summary>
            private string discountPrice = "0";

            /// <summary>
            /// sDiscountFlag
            /// </summary>
            private string discountFlag = "0";

            /// <summary>
            /// 应用的信息条文
            /// </summary>
            private string message1 = "1";

            public int ID { get => iD; set => iD = value; }
            public int DepartmentID { get => departmentID; set => departmentID = value; }
            public int GroupID { get => groupID; set => groupID = value; }
            public string ItemCode { get => itemCode; set => itemCode = value; }
            public string Name1 { get => name1; set => name1 = value; }
            public string Name2 { get => name2; set => name2 = value; }
            public string Name3 { get => name3; set => name3 = value; }
            public string Label1ID { get => label1ID; set => label1ID = value; }
            public string Label2ID { get => label2ID; set => label2ID = value; }
            public string BarcodeType1 { get => barcodeType1; set => barcodeType1 = value; }
            public string BarcodeType2 { get => barcodeType2; set => barcodeType2 = value; }
            public string UnitID { get => unitID; set => unitID = value; }
            public string Price { get => price; set => price = value; }
            public string FreshnessDate { get => freshnessDate; set => freshnessDate = value; }
            public string PackageRange { get => packageRange; set => packageRange = value; }
            public string PackageType { get => packageType; set => packageType = value; }
            public string PackageWeight { get => packageWeight; set => packageWeight = value; }
            public string Text1ID { get => text1ID; set => text1ID = value; }
            public string Text2ID { get => text2ID; set => text2ID = value; }
            public string Text3ID { get => text3ID; set => text3ID = value; }
            public string Text4ID { get => text4ID; set => text4ID = value; }
            public string DiscountID { get => discountID; set => discountID = value; }
            public string Flag1 { get => flag1; set => flag1 = value; }
            public string Flag2 { get => flag2; set => flag2 = value; }
            public string TareID { get => tareID; set => tareID = value; }
            public string ICEValue { get => iCEValue; set => iCEValue = value; }
            public int ProducedDate { get => producedDate; set => producedDate = value; }
            public int PackageDays { get => packageDays; set => packageDays = value; }
            public int PackageHours { get => packageHours; set => packageHours = value; }
            public string Flag3 { get => flag3; set => flag3 = value; }
            public string OriginID { get => originID; set => originID = value; }
            public string DiscountRate { get => discountRate; set => discountRate = value; }
            public string TareValue { get => tareValue; set => tareValue = value; }
            public string HalfDiscount { get => halfDiscount; set => halfDiscount = value; }
            public string QuarterDiscount { get => quarterDiscount; set => quarterDiscount = value; }
            public string Tax1 { get => tax1; set => tax1 = value; }
            public string Tax2 { get => tax2; set => tax2 = value; }
            public int ValidDate { get => validDate; set => validDate = value; }
            public string Text5ID { get => text5ID; set => text5ID = value; }
            public string Text6ID { get => text6ID; set => text6ID = value; }
            public string Text7ID { get => text7ID; set => text7ID = value; }
            public string Text8ID { get => text8ID; set => text8ID = value; }
            public string LimitPrice { get => limitPrice; set => limitPrice = value; }
            public string TraceabilityCode { get => traceabilityCode; set => traceabilityCode = value; }
            public string PackagePrice { get => packagePrice; set => packagePrice = value; }
            public string ProducedDateRule { get => producedDateRule; set => producedDateRule = value; }
            public string PackageDateFrom { get => packageDateFrom; set => packageDateFrom = value; }
            public string FreshnessDateFrom { get => freshnessDateFrom; set => freshnessDateFrom = value; }
            public string ValidDateFrom { get => validDateFrom; set => validDateFrom = value; }
            public string DiscountBeginTime { get => discountBeginTime; set => discountBeginTime = value; }
            public string DiscountEndTime { get => discountEndTime; set => discountEndTime = value; }
            public string DiscountPrice { get => discountPrice; set => discountPrice = value; }
            public string DiscountFlag { get => discountFlag; set => discountFlag = value; }
            public string Message1 { get => message1; set => message1 = value; }

            public override string ToString()
            {
                return $@"{ID}	{ DepartmentID}	{ GroupID}	{ ItemCode}	{ Name1}	{ Name2}	{ Name3}	{ Label1ID}	{ Label2ID}	{ BarcodeType1}	{ BarcodeType2}	{ UnitID}	{ Price}	{ FreshnessDate}	{ PackageRange}	{ PackageType}	{ PackageWeight}	{ Text1ID}	{ Text2ID}	{ Text3ID}	{ Text4ID}	{ DiscountID}	{ Flag1}	{ Flag2}	{ TareID}	{ ICEValue}	{ ProducedDate}	{ PackageDays}	{ PackageHours}	{ Flag3}	{ OriginID}	{ DiscountRate}	{ TareValue}	{ HalfDiscount}	{ QuarterDiscount}	{ Tax1}	{ Tax2}	{ ValidDate}	{ Text5ID}	{ Text6ID}	{ Text7ID}	{ Text8ID}	{ LimitPrice}	{ TraceabilityCode}	{ PackagePrice}	{ ProducedDateRule}	{ PackageDateFrom}	{ FreshnessDateFrom}	{ ValidDateFrom}	{ DiscountBeginTime}	{ DiscountEndTime}	{ DiscountPrice}	{ DiscountFlag}	{ Message1}";
            }

            public string GetTitle()
            {
                return $@"{nameof(ID)}	{nameof(DepartmentID)}	{nameof(GroupID)}	{nameof(ItemCode)}	{nameof(Name1)}	{nameof(Name2)}	{nameof(Name3)}	{nameof(Label1ID)}	{nameof(Label2ID)}	{nameof(BarcodeType1)}	{nameof(BarcodeType2)}	{nameof(UnitID)}	{nameof(Price)}	{nameof(FreshnessDate)}	{nameof(PackageRange)}	{nameof(PackageType)}	{nameof(PackageWeight)}	{nameof(Text1ID)}	{nameof(Text2ID)}	{nameof(Text3ID)}	{nameof(Text4ID)}	{nameof(DiscountID)}	{nameof(Flag1)}	{nameof(Flag2)}	{nameof(TareID)}	{nameof(ICEValue)}	{nameof(ProducedDate)}	{nameof(PackageDays)}	{nameof(PackageHours)}	{nameof(Flag3)}	{nameof(OriginID)}	{nameof(DiscountRate)}	{nameof(TareValue)}	{nameof(HalfDiscount)}	{nameof(QuarterDiscount)}	{nameof(Tax1)}	{nameof(Tax2)}	{nameof(ValidDate)}	{nameof(Text5ID)}	{nameof(Text6ID)}	{nameof(Text7ID)}	{nameof(Text8ID)}	{nameof(LimitPrice)}	{nameof(TraceabilityCode)}	{nameof(PackagePrice)}	{nameof(ProducedDateRule)}	{nameof(PackageDateFrom)}	{nameof(FreshnessDateFrom)}	{nameof(ValidDateFrom)}	{nameof(DiscountBeginTime)}	{nameof(DiscountEndTime)}	{nameof(DiscountPrice)}	{nameof(DiscountFlag)}	{nameof(Message1)}";
            }
        }

        /// <summary>
        /// 顶尖热键下发数据格式
        /// </summary>
        public class BaseHotKeyStructure : BaseStructure
        {
            private string _ButtonIndex;

            private string _ButtonValue;

            public string ButtonIndex { get => _ButtonIndex; set => _ButtonIndex = value; }
            public string ButtonValue { get => _ButtonValue; set => _ButtonValue = value; }

            public override string ToString()
            {
                return $"{_ButtonIndex} {_ButtonValue}";
            }

            public string GetTitle()
            {
                return $"{nameof(ButtonIndex)}  {nameof(ButtonValue)}";
            }
        }

        /// <summary>
        /// 备注数据结构
        /// </summary>
        public class BaseMemoStructure : BaseStructure
        {
            /// <summary>
            /// ID
            /// </summary>
            private string _PLUID;

            /// <summary>
            /// values
            /// </summary>
            private string _Value;

            public string PLUID { get => _PLUID; set => _PLUID = value; }
            public string Value { get => _Value; set => _Value = value; }

            public override string ToString()
            {
                return $"{_PLUID}   {_Value}";
            }
            public string GetTitle()
            {
                return $"{nameof(PLUID)}   {nameof(Value)}";
            }
        }

        /// <summary>
        /// 信息一
        /// </summary>
        public class BaseMeassageStructure : BaseStructure
        {
            private string _ID;

            private string _Val;

            public string ID { get => _ID; set => _ID = value; }
            public string Val { get => _Val; set => _Val = value; }

            public string GetTitle()
            {
                return $"{nameof(ID)}	{nameof(Val)}";
            }

            public override string ToString()
            {
                return $"{_ID}  {_Val}";
            }
        }

        #endregion

        /// <summary>
        /// 生成下发文本
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        private static StringBuilder InitSendTextData(List<BaseStructure> dataList)
        {
            StringBuilder sb = new StringBuilder();
            BaseStructure simple = dataList.First();
            string title = simple.GetTitle();
            StringBuilder builder = new StringBuilder();
            builder.Append(title );
            builder.Append("\r\n");
            foreach (BaseStructure item in dataList)
            {
                string cell = item.ToString();
                builder.Append(cell);
                builder.Append("\r\n");
            }
            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\zxq\Desktop\ce\ss.txt", true))
            //{
            //    file.WriteLine($"{builder}");
            //    //file.Write(line);
            //}
            return builder;
        }

        static void Main(string[] args)
        {
            aa();
            try
            {

                string dd = "524401418738681477";

                int d = ExplainUserID(dd);
                #region DataTable lambda

                //DataTable dt = new DataTable();
                //dt.Columns.Add("a");
                //dt.Columns.Add("b");
                //dt.Columns.Add("c");

                //for (int i = 0; i < 10; i++)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["a"] = "2";
                //    dr["b"] = "2";
                //    dr["c"] = "2";
                //    dt.Rows.Add(dr);
                //}

                //int t = 0;
                //foreach (DataRow item in dt.Rows)
                //{
                //    t++;
                //    item["a"] = t;
                //}

                //DataRow[] ddd = dt.Select("1 = 1");
                //DataTable products = dt;
                //IEnumerable<DataRow> rows = from p in products.AsEnumerable()
                //                            select p;

                //DataTable newdata = ddd[0].Table.Clone();
                //foreach (DataRow item in ddd)
                //{
                //    newdata.ImportRow(item);
                //}
                //newdata.Rows[0]["a"] = "ppp";
                //DataView dataView = dt.DefaultView;
                //DataTable dataTableDistinct = dataView.ToTable(true, "a", "b");
                //string da = "-1";
                //string dd = "+1";
                //decimal dfg = Convert.ToDecimal(dd);
                #endregion

                #region TimeSpan

                //int systemDef = 0;
                //DateTime dtNow = DateTime.Now;
                //string data = "11:30:00";
                //DateTime de = Convert.ToDateTime(data);

                //TimeSpan timeDividingLine = new TimeSpan(de.Hour ,de.Minute ,de.Second); // new TimeSpan(11, 30, 0);
                //if (dtNow.TimeOfDay < timeDividingLine)
                //{
                //    //上午
                //    systemDef = 619;
                //}
                //else
                //{
                //    //下午
                //    systemDef = 631;
                //}

                #endregion

                #region 引用类型测试
                //List<List<string>> da = new List<List<string>>();
                //List<string> d = new List<string>() { "1"};
                //da.Add(d);
                //d = new List<string>();
                #endregion

                #region DataTable 应用类型
                //DataTable dt = new DataTable();
                //dt.Columns.Add("a");
                //dt.Columns.Add("b");
                //dt.Columns.Add("c");

                //for (int i = 0; i < 10; i++)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["a"] = "2";
                //    dr["b"] = "2";
                //    dr["c"] = "2";
                //    dt.Rows.Add(dr);
                //}

                //int t = 0;
                //foreach (DataRow item in dt.Rows)
                //{
                //    t++;
                //    item["a"] = t;
                //}

                //int o = 1;
                //DataRow[] ddd = dt.Select("a = '2'");

                //ddd[0]["a"] = "999";
                ////DataTable products = dt;
                #endregion

                #region 反射 Dynamic (Dictionary)

                //List<string> ddrew = new List<string>() { "aa", "bb" };
                //List<dynamic> all = new List<dynamic>()
                //{
                //     new  { aa = "1" ,bb = "2" ,cc = "3"} ,
                //     new  { aa = "2" ,bb = "22" ,cc = "222"} ,
                //};

                //List<dynamic> res = new List<dynamic>();

                //string a = JsonConvert.SerializeObject(all);

                //foreach (dynamic item in all)
                //{

                //    string dddff = "{\"billdate\":\"2019 - 02 - 25T00: 00:00\",\"总销售额\":74679.0477,\"厂供销售额\":74679.0477,\"销售额\":74679.0477}";
                //    string ssss = JsonConvert.SerializeObject(item);
                //    Dictionary<string, object> ddddf = JsonConvert.DeserializeObject<Dictionary<string ,object>>(ssss);
                //    foreach (KeyValuePair<string , object> av in ddddf)
                //    {

                //    }

                //    object dhd = ddddf["aa"];
                //    object cell = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(dddff);
                //    var d = cell.GetType();
                //    var dfg = cell.ToString();

                //    Type ts = cell.GetType();
                //    dynamic info = new System.Dynamic.ExpandoObject();
                //    var dic = (IDictionary<string, object>)info;

                //    foreach (PropertyInfo itemf in ts.GetProperties())
                //    {
                //        if (ddrew.Contains(itemf.Name))
                //        {
                //            dic.Add(itemf.Name, itemf.GetValue(item, null));
                //            //sSetValue.Append($"{propertyInfo.Name} = '{propertyInfo.GetValue(entity, null)}' ,");
                //        }
                //    }

                //    foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
                //    {
                //        if (ddrew.Contains(propertyInfo.Name))
                //        {
                //            dic.Add(propertyInfo.Name, propertyInfo.GetValue(item, null));
                //            //sSetValue.Append($"{propertyInfo.Name} = '{propertyInfo.GetValue(entity, null)}' ,");
                //        }
                //    }
                //    res.Add(info);
                //}

                #endregion

                #region 异常
                //try
                //{
                //    throw new Verification1Exception("23");
                //}
                //catch (Verification2Exception ex)
                //{
                //    Console.WriteLine("321123" + ex.Message);
                //}
                //catch (Verification1Exception ex)
                //{
                //    Console.WriteLine("321123" + ex.Message);
                //}
                //catch (ArgumentException ex)
                //{
                //    Console.WriteLine("321123" + ex.Message);
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("321123" + ex.Message);
                //}
                //Console.WriteLine("123");
                #endregion

                #region 获取主机名
                //string b = Dns.GetHostName();
                //string d = Dns.GetHostEntry("localhost").HostName;
                //string c = Environment.GetEnvironmentVariable("computername");
                #endregion

                #region 获取路径 
                //F:\test2\ConsoleApp1\ConsoleApp4\bin\Debug\ConsoleApp4.exe
                string file = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                //F:\test2\ConsoleApp1\ConsoleApp4\bin\Debug\ConsoleApp4.exe
                string file1 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                //F:\test2\ConsoleApp1\ConsoleApp4\bin\Debug
                string file2 = System.Environment.CurrentDirectory;
                //F:\test2\ConsoleApp1\ConsoleApp4\bin\Debug
                string file3 = System.IO.Directory.GetCurrentDirectory();
                //F:\test2\ConsoleApp1\ConsoleApp4\bin\Debug\
                string file4 = System.AppDomain.CurrentDomain.BaseDirectory;
                //F:\test2\ConsoleApp1\ConsoleApp4\bin\Debug\
                string file5 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                //string file6 = System.Windows.Forms.Application.StartupPath;
                //string file7 = System.Windows.Forms.Application.ExecutablePath;


                #endregion

                #region Timer间隔执行

                //System.Threading.Timer
                //threadTimer = new System.Threading.Timer(new System.Threading.TimerCallback(Method3),
                //null,
                //0, 5000);
         //       while (true)
         //       {
         //           Console.WriteLine("test_" +
         //Thread.CurrentThread.ManagedThreadId.ToString());
         //           Thread.Sleep(100);
         //       }

                Console.ReadLine();


                #endregion


            }
            catch (Exception ex)
            {
                Console.WriteLine("321123" + ex.Message);
            }
            finally
            {
                Console.WriteLine("a");
            }
        }
        public static int ExplainUserID(string key)
        {
            string firstCode = key.Substring(0,1);
            switch (firstCode)
            {
                case "A":
                    firstCode = "10";
                    break;
                case "B":
                    firstCode = "11";
                    break;
                case "C":
                    firstCode = "12";
                    break;
                default:
                    break;
            }
            int count = Convert.ToInt32(firstCode) - 3;
            int ID = Convert.ToInt32(key.Substring(1,  count));
            return ID;
        }


        public static void Method3(Object
            state)
        {
            Console.WriteLine(DateTime.Now.ToString()
     + "_" +
     Thread.CurrentThread.ManagedThreadId.ToString());
        }


        #region Verification
        public class Verification1Exception : ApplicationException
        {
            //public MyException (){}
            public Verification1Exception(string message) : base(message)
            { }//这句话知道是干的吧?别和我说你忘了!!
            public override string Message
            {
                get
                {
                    return base.Message;
                }
            }
        }
        public class Verification2Exception : ApplicationException
        {
            //public MyException (){}
            public Verification2Exception(string message) : base(message)
            { }//这句话知道是干的吧?别和我说你忘了!!
            public override string Message
            {
                get
                {
                    return base.Message;
                }
            }
        }

        public class Verification3Exception : System.ApplicationException
        {

        }
        #endregion
    }
  

}
