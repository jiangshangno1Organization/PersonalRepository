using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Service;

namespace WebFinalApi.Service
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(CommonDB dB) : base(dB)
        {
        }

        /// <summary>
        /// 获取用户购物车信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<OrderCart> GetUserCart(int userID)
        {
            //通过userID 获取用户购物车信息
            var cartData = GetCartsDataByUserID(userID);
            return cartData;
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
            if (goodsData.stockLimit.Equals("1") && goodsData.stock < count)
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
            string sqlCondition = InsertSqlGenerate<OrderCart>(orderCart, new List<string>()
            {
                nameof(orderCart.userID),nameof(orderCart.gdsID),nameof(orderCart.count),nameof(orderCart.addTime)
            });
            string sql = $@"IF NOT EXISTS (SELECT 1 FROM order_cart WHERE userID = @userID AND gdsID = @gdsID)
                INSERT INTO order_cart (userID,gdsID,count,addtime) VALUES(@userID,@gdsID,@count,@addTime)  
                ELSE UPDATE order_cart set count = count + @count  WHERE userID = @userID and gdsid = @gdsID";
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
                sqlCondition =  DeleteSqlGenerate(cartSimple, new List<string>() {nameof(cartSimple.userID) });
            }
            else
            {
                cartSimple.gdsID = gdsID;
                //删除购物车所有
                sqlCondition = DeleteSqlGenerate(cartSimple, new List<string>() { nameof(cartSimple.userID) , nameof(cartSimple.gdsID) });
            }
            string sql = $"DELETE TABLE order_cart {sqlCondition}";
            if (commonDB.Excute(sql, cartSimple) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 购物车提交订单
        /// </summary>
        /// <returns></returns>
        public int SubmitOrderByCart(int userID, List<int> IDs, bool ifSumbitAll = false)
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
                GenerateOrderForCarts(orderCarts, goodsData, user);

                commonDB.Commit();


            try
            {
            }
            catch (VerificationException ex)
            {
                commonDB.Rollback();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Verification商品相关 （库存）
        /// </summary>
        /// <param name="cartData"></param>
        /// <param name="goodsIDs"></param>
        /// <param name="fileCartIDs"></param>
        private void VerificationGoodsABT(ref List<OrderCart> cartData, List<Goods> goodsIDs, ref List<int> fileCartIDs)
        {
            for (int i = cartData.Count; i > 0; i--)
            {
                int gdsId = cartData[i].gdsID;
                int needCount = cartData[i].count;
                Goods goods = goodsIDs.Where(c => c.goodsID == gdsId).First();
                if (goods.stockLimit.Equals("1") && goods.stock < needCount)
                {
                    //库存现在 并 不足
                    fileCartIDs.Add(cartData[i].ID);
                    cartData.RemoveAt(i);
                }
            }
        }


        private void GenerateOrderForCarts(List<OrderCart> carts ,List<Goods> goodsDatas,Users user)
        {
            //生成订单ID
            int orderID = GetCode("orderid");

            //生成订单详情
            var priceDatas = from goods in goodsDatas
                            join cart in carts on goods.goodsID equals cart.gdsID
                            select new OrderDetail
                            {
                                gdsID = goods.goodsID,
                                gdsCD = goods.goodsCD,
                                baseID = orderID,
                                count = cart.count,
                                unitprice = goods.aPrice,
                                allprice = cart.count * goods.aPrice
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
                baseID = orderID
            };
            //执行生存
            GenerateOrder(orderBase, priceDatas);

        }

        public int PayOrder(int orderID)
        {
            throw new NotImplementedException();
        }

  
    }
}