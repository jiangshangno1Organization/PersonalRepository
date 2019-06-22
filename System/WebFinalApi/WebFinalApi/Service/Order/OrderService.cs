using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Models.Goods;
using WebFinalApi.Models.Order;
using WebFinalApi.Service;

namespace WebFinalApi.Service
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(CommonDB dB) : base(dB)
        {
        }

        #region 购物车相关

        /// <summary>
        /// 获取用户购物车信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public OrderCartDto GetUserCart(int userID)
        {
            //通过userID 获取用户购物车信息
            var cartData = GetCartsDataByUserID(userID);

            //获取商品信息
            var goodsData = GetGoodsDataByGoodsIDs(cartData.Select(i => i.gdsID));

            //图片信息
            var goodsPicture = GetGoodsPictures(cartData.Select(i => i.gdsID), true);
            //
            IEnumerable<GoodsCell> dd = from goodsDataCell in goodsData
                                        join goodsPictureCell in goodsPicture on goodsDataCell.goodsID equals goodsPictureCell.goodsId
                                        select new GoodsCell()
                                        {
                                            goodsID = goodsDataCell.goodsID,
                                            goodsName = goodsDataCell.goodsName,
                                            goodsCD = goodsDataCell.goodsCD,
                                            price = goodsDataCell.price,
                                            aPrice = goodsDataCell.aPrice,
                                            goodsPictrures = new List<GoodsPictrure>()
                                            {
                                                new GoodsPictrure()
                                                {
                                                     file =   goodsPictureCell.file,
                                                      key =   goodsPictureCell.file
                                                }
                                            }
                                        };

            return new OrderCartDto()
            {
                orderCarts = cartData.Select(i => new CarCell()
                {
                    carID = i.ID,
                    goodsCell = dd.Where(q => q.goodsID == i.gdsID).First(),
                    goodsCount = i.count,
                    memo = i.memo
                }).ToList()
            };
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gdsID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool ADDToCart(int userID, int gdsID, int count)
        {
            //找到商品
            var goodsData = GetGoodsDataBygoodsID(gdsID);
            //状态、库存检查
            if (goodsData == null)
            {
                throw new VerificationException("该商品不存在.");
            }
            //若限制库存 判断库存是否足够
            if (!String.IsNullOrWhiteSpace(goodsData.stockLimit) && goodsData.stockLimit.Equals("1") && goodsData.stock < count)
            {
                throw new VerificationException("库存不足够.");
            }
            //添加到购物车
            OrderCart orderCart = new OrderCart()
            {
                addTime = GetSystemTime(),
                count = count,
                gdsID = gdsID,
                userID = userID
            };
       
            string sql = $@"IF NOT EXISTS (SELECT 1 FROM order_cart WHERE userID = @userID AND gdsID = @gdsID)
                INSERT INTO order_cart (userID,gdsID,count,addtime) VALUES(@userID,@gdsID,@count,GETDATE())  
                ELSE UPDATE order_cart set count = count + @count , addtime = GETDATE() WHERE userID = @userID and gdsid = @gdsID";
            return commonDB.Excute(sql, orderCart) == 1;
        }

        /// <summary>
        /// 购物车删除
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gdsID"></param>
        /// <param name="ifRemoveAll"></param>
        /// <returns></returns>
        public bool RemoveCart(int userID, int gdsID, bool ifRemoveAll = false)
        {
            OrderCart cartSimple = new OrderCart()
            {
                userID = userID
            };
            string sqlCondition = string.Empty;
            if (ifRemoveAll)
            {
                //删除购物车所有
                sqlCondition = DeleteSqlGenerate(cartSimple, new List<string>() { nameof(cartSimple.userID) });
            }
            else
            {
                cartSimple.gdsID = gdsID;
                //删除购物车所有
                sqlCondition = DeleteSqlGenerate(cartSimple, new List<string>() { nameof(cartSimple.userID), nameof(cartSimple.gdsID) });
            }
            string sql = $"DELETE order_cart {sqlCondition}";
            if (commonDB.Excute(sql, cartSimple) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 订单相关

        /// <summary>
        /// 购物车提交订单
        /// </summary>
        /// <returns></returns>
        public OrderSubmitDto SubmitOrderByCart(int userID, List<int> IDs, bool ifSumbitAll = false)
        {
            string remindMsg = string.Empty;
            bool res = false;
            int orderID = -1;
            try
            {
                //获取用户信息
                var user = GetUserDataByUserID(userID);
                List<OrderCart> orderCarts = null;

                //事物开启
                commonDB.BeginTrans();
                //获取待提交商品信息
                if (ifSumbitAll)
                {
                    orderCarts = GetCartsDataByUserID(userID).ToList();
                }
                else
                {
                    orderCarts = GetCartsDataByCartIDs(IDs).ToList();
                }
                var goodsData = GetGoodsDataByGoodsIDs(orderCarts.Select(i => i.gdsID)).ToList();
                List<int> fileCartIDs = new List<int>();

                //状态、库存验证
                VerificationGoodsABT(ref orderCarts, goodsData, ref fileCartIDs);

                //生成待支付订单
                orderID = GenerateOrderForCarts(orderCarts, goodsData, user);
                commonDB.Commit();
                res = true;
            }
            catch (VerificationException ex)
            {
                remindMsg = ex.Message;
                commonDB.Rollback();
            }
            catch (Exception ex)
            {
                commonDB.Rollback();
                throw ex;
            }
            return new OrderSubmitDto()
            {
                baseOrderID = orderID,
                ifSuccess = res,
                remindMsg = remindMsg
            };
        }

        /// <summary>
        /// Verification商品相关 （库存）
        /// </summary>
        /// <param name="cartData"></param>
        /// <param name="goodsIDs"></param>
        /// <param name="fileCartIDs"></param>
        private void VerificationGoodsABT(ref List<OrderCart> cartData, List<Goods> goodsIDs, ref List<int> fileCartIDs)
        {
            for (int i = cartData.Count - 1; i >= 0; i--)
            {
                int gdsId = cartData[i].gdsID;
                int needCount = cartData[i].count;
                Goods goods = goodsIDs.Where(c => c.goodsID == gdsId).First();
                if (!string.IsNullOrWhiteSpace(goods.stockLimit) && goods.stockLimit.Equals("1") && goods.stock < needCount)
                {
                    //库存现在 并 不足
                    fileCartIDs.Add(cartData[i].ID);
                    cartData.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="carts"></param>
        /// <param name="goodsDatas"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private int GenerateOrderForCarts(List<OrderCart> carts, List<Goods> goodsDatas, Users user)
        {
            //生成订单ID
            int orderID = GetCode("orderid");

            //图片
            var goodsPic = GetGoodsPictures(goodsDatas.Select(i => i.goodsID), true);

            //生成订单详情
            var priceDatas = from goods in goodsDatas
                             join cart in carts on goods.goodsID equals cart.gdsID
                             join goodsPicCell in goodsPic on goods.goodsID equals goodsPicCell.goodsId into q
                             from pic in q.DefaultIfEmpty()
                             select new OrderDetail
                             {
                                 gdsID = goods.goodsID,
                                 gdsCD = goods.goodsCD,
                                 gdsName = goods.goodsName,
                                 baseID = orderID,
                                 count = cart.count,
                                 unitprice = goods.aPrice,
                                 allprice = cart.count * goods.aPrice,
                                 goodspic = pic == null ? "" : pic.file
                             };

            //生成订单基础
            OrderBase orderBase = new OrderBase()
            {
                ifdel = "0",
                ifpay = "0",
                inittime = GetSystemTime(),
                userID = user.userId,
                mobile = user.mobile,
                status = "LR",
                userName = user.userName,
                sum = priceDatas.Sum(c => c.allprice),
                baseID = orderID,
                goodsnumber = priceDatas.Sum(c => c.count)
            };
            //执行生存
            GenerateOrder(orderBase, priceDatas);
            return orderID;

        }

        #endregion

        #region 订单数据获取

        private string GetOrderStatusText(string bcd)
        {
            string status = string.Empty;
            switch (bcd)
            {
                case "LR":
                    status = "未支付";
                    break;
                case "SX":
                    status = "待收货";
                    break;
                case "FH":
                    status = "已完成";
                    break;
                case "QX":
                    status = "已取消";
                    break;
                default:
                    break;
            }
            return status;
        }

        /// <summary>
        /// 获取订单列表（0：未付款 1：未收货 2：已完成 3:已取消）
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<OrderDataDto> GetOrderList(int userID, string type)
        {
            string status = string.Empty;
            switch (type)
            {
                case "0":
                    status = "QD";
                    break;
                case "1":
                    status = "SX";
                    break;
                case "2":
                    status = "FH";
                    break;
                case "3":
                    status = "QX";
                    break;
                default:
                    break;
            }
            string statusCondition = string.Empty;
            if (!string.IsNullOrWhiteSpace(status))
            {
                statusCondition = " AND status = @status ";
            }
            string sql = $"SELECT * FROM order_base WHERE userid = @id AND ifdel = '0' {statusCondition} ORDER BY ID DESC";
            var orderBase = commonDB.Query<OrderBase>(sql, new { id = userID, status = status });

            if (orderBase != null && orderBase.Count() > 0)
            {
                //orderDetail
                sql = $"SELECT * FROM order_detail WHERE baseid in @baseids ";
                var orderDetail = GetOrderDetailsByBaseIDs(orderBase.Select(i => i.baseID));
                //组装
                return orderBase.Select(i => new OrderDataDto()
                {
                    orderDate = i.inittime.ToString("yyyy-MM-dd"),
                    orderStatus = GetOrderStatusText(i.status),
                    address = i.address1,
                    baseID = i.baseID,
                    factSum = i.factNum,
                    mobile = i.mobile,
                    sum = i.sum,
                    userName = i.userName,
                    goodsNumber = i.goodsnumber,
                    orderDetails = orderDetail.Where(t => t.baseID.Equals(i.baseID)).Select(t => new OrderDetailDto()
                    {
                        baseID = t.baseID,
                        allPrice = t.allprice,
                        goodsCD = t.gdsCD,
                        goodsID = t.gdsID,
                        goodsName = t.gdsName,
                        unitPrice = t.unitprice,
                        goodsCount = t.count,
                        goodsPic = t.goodspic
                        //picture = t.pic

                    }).ToList()
                }).ToList();
            }
            else
            {
                return new List<OrderDataDto>() { };
            }

        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="baseID"></param>
        /// <returns></returns>
        public OrderDataDto GetOrderDetail(int baseID)
        {
            var baseData = GetOrderBaseByBaseID(baseID);
            var detailData = GetOrderDetailsByBaseIDs(new List<int>() { baseID });

            if (baseData != null && detailData != null && detailData.Count() > 0)
            {

            }
            else
            {
                throw new VerificationException("订单数据不存在.");
            }


            OrderDataDto order = new OrderDataDto()
            {
                address = baseData.address1,
                baseID = baseData.baseID,
                orderStatus = GetOrderStatusText(baseData.status),
                factSum = baseData.factNum,
                mobile = baseData.mobile,
                sum = baseData.sum,
                userName = baseData.userName,
                orderDetails = detailData.Select(i => new OrderDetailDto()
                {
                    allPrice = i.allprice,
                    baseID = i.baseID,
                    goodsCD = i.gdsCD,
                    goodsID = i.gdsID,
                    goodsName = i.gdsName,
                    unitPrice = i.unitprice,
                    goodsCount = i.count,
                    goodsPic = i.goodspic
                }).ToList()
            };
            return order;
        }

        #endregion

        public int PayOrder(int orderID)
        {
            throw new NotImplementedException();
        }


        public bool EffectiveOrder(int orderID)
        {
            //状态更新 QD => SX


            //库存扣减


            return true;
        }

    }
}