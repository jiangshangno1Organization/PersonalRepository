// <summary>
// Description      :
// Author           : zxq
// Create Time      : 2019/3/30 14:54:24
// Revision history : （序号，修改内容，修改人，修改时间）
// 1、  2019/3/30 14:54:24
// </summary>

using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFinalApi.Service
{
    public class SystemService : ISystemService
    {
        CommonDB commonDB ;
        public SystemService(CommonDB dB)
        {
            commonDB = dB;
        }
        public DateTime GetSystemTime()
        {
            DateTime dt = commonDB.ExecuteScalar<DateTime>("SELECT GETDATE()");
            return dt;
        }
        public T GetT<T>()
        {
            dynamic dynamic = commonDB.Query<dynamic>("Select * from ec_info_cls where ifdel = @ifdel" , new {ifdel = "0" });
            return dynamic;
        }

    }
}
