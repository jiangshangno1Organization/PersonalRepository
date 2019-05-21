using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Models.Goods;

namespace WebFinalApi.Service
{
    public class BaseService
    {
        public CommonDB commonDB;

        public BaseService(CommonDB dB)
        {
            commonDB = dB;
        }

        #region 获取系统设置

        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public SystemSet GetSystemSet(int ID)
        {
            string sql = $"SELECT * FROM sys_set WHERE id = {ID}";
            return commonDB.QueryFirstOrDefault<SystemSet>(sql, new { id = ID });
        }

        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetSystemTime()
        {
            DateTime dt = commonDB.ExecuteScalar<DateTime>("SELECT GETDATE()");
            return dt;
        }

        /// <summary>
        /// 获取自增ID
        /// </summary>
        /// <param name="codeKey"></param>
        /// <returns></returns>
        public int GetCode(string codeKey)
        {
            string sql = $"SELECT  value FROM  sys_code WHERE codekey = '{codeKey}' UPDATE sys_code set value = value+1 WHERE codekey = '{codeKey}'";
            return commonDB.ExecuteScalar<int>(sql);
        }

        #endregion

        #region 分类信息

        /// <summary>
        /// 获取Category 
        /// </summary>
        /// <param name="CategoryCD"></param>
        /// <returns></returns>
        public Category GetCategory(string CategoryCD)
        {
            var datas = GetCategoriesByCategoryCD(new List<string>() { CategoryCD });
            if (datas != null && datas.Count() > 0)
            {
                return datas.First();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取Categories 通过CDs
        /// </summary>
        /// <param name="CategoryCDs"></param>
        /// <returns></returns>
        public IEnumerable<Category> GetCategoriesByCategoryCD(IEnumerable<string> CategoryCDs)
        {
            string sql = $"SELECT * FROM goods_classification WHERE ifdel = '0' AND CD IN @cds";
            return commonDB.Query<Category>(sql, new { cds = CategoryCDs });
        }

        #endregion

        #region 商品基础信息获取

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Goods> GetAllGoods()
        {
            string sql = "SELECT * FROM goods_data WHERE ifdel = '0'";
            return commonDB.Query<Goods>(sql);
        }

        /// <summary>
        /// 获取商品信息 通过 CategoryIDs
        /// </summary>
        /// <param name="CategoryIDs"></param>
        /// <returns></returns>
        public IEnumerable<Goods> GetGoodsByCategoryIDs(IEnumerable<int> CategoryIDs)
        {
            string sql = "SELECT * FROM goods_data WHERE classid in @ids AND ifdel = '0'";
            return commonDB.Query<Goods>(sql, new { ids = CategoryIDs });
        }

        /// <summary>
        /// 获取多个商品 通过商品ID
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public IEnumerable<Goods> GetGoodsDataByGoodsIDs(IEnumerable<int> GoodsIDs)
        {
            string sql = "SELECT * FROM goods_data  WHERE goodsid IN @goodsids AND ifdel = '0'";
            return commonDB.Query<Goods>(sql, new { goodsids = GoodsIDs });
        }

        /// <summary>
        /// 获取单个商品
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public Goods GetGoodsDataBygoodsID(int ID)
        {
            string sql = "SELECT * FROM goods_data  WHERE goodsid = @goodsid AND ifdel = '0'";
            return commonDB.QueryFirstOrDefault<Goods>(sql, new { goodsid = ID });
        }

        /// <summary>
        /// 获取商品图片
        /// </summary>
        /// <param name="goodsIDs"></param>
        /// <returns></returns>
        public IEnumerable<GoodsPicture> GetGoodsPictures(IEnumerable<int> goodsIDs, bool ifDefault)
        {
            string defaultCondition = ifDefault ? " AND ifdefault = '1'" : "";
            string sql = $"SELECT * FROM goods_picture WHERE goodsid IN @ids AND ifdel = '0' {defaultCondition}";
            return commonDB.Query<GoodsPicture>(sql, new { ids = goodsIDs });
        }

        /// <summary>
        /// 商品组装方法
        /// </summary>
        /// <param name="goodsIDs"></param>
        /// <returns></returns>
        public GoodsDataOutput GenerateGoods(IEnumerable<int> goodsIDs)
        {
            //基础信息
            var goodsData = GetGoodsDataByGoodsIDs(goodsIDs);
            //商品图片
            var goodsPicture = GetGoodsPictures(goodsData.Select(i => i.goodsID), true);
            //组装
            var goodsDto = from goods in goodsData
                           join picture in goodsPicture on goods.goodsID equals picture.goodsId
                           select new GoodsCell()
                           {
                               goodsID = goods.goodsID,
                               aPrice = goods.aPrice,
                               price = goods.price,
                               classID = goods.classID,
                               goodsCD = goods.goodsCD,
                               goodsName = goods.goodsName,
                               text1 = goods.text1,
                               text2 = goods.text2,
                               goodsPictrures = new List<GoodsPictrure>()
                               {
                                 new GoodsPictrure()
                                 {
                                      file = picture.file,
                                      key = picture.file
                                 }
                               }
                           };

            GoodsDataOutput output = new GoodsDataOutput()
            {
                goods = goodsDto.ToList(),
                goodsCount = goodsDto.Count()
            };
            return output;
        }

        #endregion

        #region 订单相关

        /// <summary>
        /// 获取购物车信息 通过用户ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<OrderCart> GetCartsDataByUserID(int userID)
        {
            OrderCart orderCart = new OrderCart()
            {
                userID = userID
            };
            string sqlCondition = SelectSqlGenerate<OrderCart>(orderCart, new List<string>() { nameof(orderCart.userID) });
            string sql = $"SELECT * FROM order_cart {sqlCondition} ";
            return commonDB.Query<OrderCart>(sql, orderCart);
        }

        /// <summary>
        /// 获取购物车信息 通过购物车ID
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public IEnumerable<OrderCart> GetCartsDataByCartIDs(IEnumerable<int> IDs)
        {
            string sql = $"SELECT * FROM order_cart WHERE ID IN @ids ";
            return commonDB.Query<OrderCart>(sql, new { ids = IDs});
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="orderBase"></param>
        /// <param name="orderDetails"></param>
        public void GenerateOrder(OrderBase orderBase ,IEnumerable<OrderDetail> orderDetails)
        {
            string sqlConditon = InsertSqlGenerate(orderBase, new List<string>()
            {
                nameof(orderBase.baseID),
                nameof(orderBase.ifdel),
                nameof(orderBase.ifpay),
                nameof(orderBase.inittime),
                nameof(orderBase.mobile),
                nameof(orderBase.status),
                nameof(orderBase.sum),
                nameof(orderBase.userID),
                nameof(orderBase.userName)
            });
            string sql = $"INSERT INTO order_base {sqlConditon} ";
            if (commonDB.Excute(sql, orderBase) != 1)
            {
                throw new OperationException("订单基础生成失败");
            }
            foreach (OrderDetail item in orderDetails)
            {
                sqlConditon = InsertSqlGenerate(item, new List<string>()
                {
                    nameof(item.baseID),
                    nameof(item.allprice),
                    nameof(item.count),
                    nameof(item.gdsCD),
                    nameof(item.gdsID),
                    nameof(item.unitprice),
                    nameof(item.gdsName)
                });
                sql = $"INSERT INTO order_detail {sqlConditon}";
                if (commonDB.Excute(sql, item) != 1)
                {
                    throw new OperationException("订单基础生成失败");
                }
            }
        }

        /// <summary>
        /// 获取订单数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Tuple<int, int> GetOrderCount(int userID)
        {
            string sql = $"SELECT COUNT(*) FROM order_base  WHERE ifdel = '0' AND status = 'LR' AND userID = '{userID}'";
            int needPay = commonDB.ExecuteScalar<int>(sql);
            sql = $"SELECT COUNT(*) FROM order_base  WHERE ifdel = '0' AND status = 'SX' AND userID = '{userID}'";
            int waitRecive = commonDB.ExecuteScalar<int>(sql);
            return new Tuple<int, int>(needPay, waitRecive);
        }

        #endregion


        #region 订单数据获取

        /// <summary>
        /// 获取订单基础数据 通过baseID
        /// </summary>
        /// <param name="baseID"></param>
        /// <returns></returns>
        public OrderBase GetOrderBaseByBaseID(int baseID)
        {
            string sql = $"SELECTY * FROM order_base WHERE baseid = @id AND ifdel = '0'";
            return commonDB.QueryFirstOrDefault<OrderBase>(sql, new { id = baseID });
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="baseIDs"></param>
        /// <returns></returns>
        public IEnumerable<OrderDetail> GetOrderDetailsByBaseIDs(IEnumerable<int> baseIDs)
        {
            string sql = "SELECT * FROM order_detail WHERE baseID IN @ids";
            return commonDB.Query<OrderDetail>(sql, new { ids = baseIDs });
        }

        #endregion

        #region 用户信息相关

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Users GetUserDataByUserID(int userID)
        {
            string sql = $"SELECT * FROM users WHERE userid = @id AND ifdel = '0'";
            return commonDB.QueryFirstOrDefault<Users>(sql, new { id = userID });
        }

        #endregion

        #region SQL构建 
        /// <summary>
        /// Insert语句生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataModel"></param>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        public string InsertSqlGenerate<T>(T dataModel, IEnumerable<string> fileNames)
        {
            string sqlResult = " VALUES ";
            var type = dataModel.GetType();//获取属性集合 
            PropertyInfo[] pi = type.GetProperties();//获取属性集合   
            string cellTitles = string.Empty;
            string cellValues = string.Empty;
            foreach (PropertyInfo p in pi)
            {
                if (fileNames.Contains(p.Name))
                {
                    try
                    {
                        cellTitles += p.Name + " ,";
                        cellValues += $" @{p.Name} ,";
                    }
                    catch
                    {
                    }
                }
            }
            return $"({cellTitles.TrimEnd(',')}) {sqlResult} ({cellValues.TrimEnd(',')})";
        }

        /// <summary>
        /// SELECT语句生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataModel"></param>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        public string SelectSqlGenerate<T>(T dataModel, IEnumerable<string> fileNames)
        {
            var type = dataModel.GetType();//获取属性集合 
            PropertyInfo[] pi = type.GetProperties();//获取属性集合   
            List<string> allConditon = new List<string>();
            foreach (PropertyInfo p in pi)
            {
                if (fileNames.Contains(p.Name))
                {
                    try
                    {
                        allConditon.Add($" {p.Name} = @{p.Name} ");
                    }
                    catch
                    {
                    }
                }
            }
            return $" WHERE {string.Join("AND", allConditon)}";
        }

        /// <summary>
        /// UPDATE语句生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataModel"></param>
        /// <param name="updateFileNames"></param>
        /// <param name="accordingFileNames"></param>
        /// <returns></returns>
        public string UpdateSqlGenerate<T>(T dataModel, IEnumerable<string> updateFileNames, IEnumerable<string> accordingFileNames)
        {
            var type = dataModel.GetType();//获取属性集合 
            PropertyInfo[] pi = type.GetProperties();//获取属性集合   
            List<string> allConditon = new List<string>();

            List<string> allConditon2 = new List<string>();

            foreach (PropertyInfo p in pi)
            {
                try
                {
                    if (updateFileNames.Contains(p.Name))
                    {

                        allConditon.Add($" {p.Name} = @{p.Name} ");

                    }
                    if (accordingFileNames.Contains(p.Name))
                    {
                        allConditon2.Add($" {p.Name} = @{p.Name} ");
                    }
                }
                catch
                {
                }
            }
            return $" SET {string.Join(",", allConditon)} WHERE {string.Join("AND", allConditon2)}";
        }

        /// <summary>
        /// DELETE语句生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataModel"></param>
        /// <param name="accordingFileNames"></param>
        /// <returns></returns>
        public string DeleteSqlGenerate<T>(T dataModel, IEnumerable<string> accordingFileNames)
        {
            return SelectSqlGenerate(dataModel, accordingFileNames);
        }

        #endregion

    }
}