// <summary>
// Description      :
// Author           : zxq
// Create Time      : 2018/8/2 9:16:43
// Revision history : （序号，修改内容，修改人，修改时间）
// 1、  2018/8/2 9:16:43
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class normal_class1
    {
        static void main()
        {

            string url = "http://218.72.252.14:10556/Authorization/Index?backUrl=Approval/ApprovalIndex&bcd=NCC&tid=9884&funcid=700&code=bf287b1c0ef13757ae5330ac9592356d&state=STATE";
            string urlB = "";
            urlB = url.Substring(url.IndexOf("backUrl=") + 8, url.Length);
            Console.WriteLine(urlB);

        }
    }
}
