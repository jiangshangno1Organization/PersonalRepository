using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
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

        public SystemSet GetSystemSet(int ID)
        {
            string sql = $"SELECT * FROM sys_set WHERE id = {ID}";
            return commonDB.QueryFirstOrDefault<SystemSet>(sql, new { id = ID });
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
            string sql = "SELECT * FROM goods_data FROM goods_data WHERE ifdel = '0'";
            return commonDB.Query<Goods>(sql);
        }

        /// <summary>
        /// 获取商品信息 通过 CategoryIDs
        /// </summary>
        /// <param name="CategoryIDs"></param>
        /// <returns></returns>
        public IEnumerable<Goods> GetGoodsByCategoryIDs(IEnumerable<string> CategoryIDs)
        {
            string sql = "SELECT * FROM goods_data WHERE classid in @ids AND ifdel = '0'";
            return commonDB.Query<Goods>(sql, new { ids = CategoryIDs });
        }

        /// <summary>
        /// 获取多个商品 通过商品ID
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public IEnumerable<Goods> GetGoodsData(IEnumerable<int> GoodsIDs)
        {
            string sql = "SELECT * FROM goods_data FROM goods_data WHERE goodsid IN @goodsids AND ifdel = '0'";
            return commonDB.Query<Goods>(sql, new { goodsids = GoodsIDs });
        }

        /// <summary>
        /// 获取单个商品
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public Goods GetGoodsData(int ID)
        {
            string sql = "SELECT * FROM goods_data FROM goods_data WHERE goodsid = @goodsid AND ifdel = '0'";
            return commonDB.QueryFirstOrDefault<Goods>(sql, new { goodsids = ID });
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
            var goodsData = GetGoodsData(goodsIDs);
            //商品图片
            var goodsPicture = GetGoodsPictures(goodsData.Select(i => i.goodsId), true);
            //组装
            var goodsDto = from goods in goodsData
                           join picture in goodsPicture on goods.goodsId equals picture.goodsId
                           select new GoodsCell()
                           {
                               goodsID = goods.goodsId,
                               aPrice = goods.aPrice,
                               price = goods.price,
                               classID = goods.classId,
                               goodsCD = goods.goodsCd,
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
        #endregion


    }
}