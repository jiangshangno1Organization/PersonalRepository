using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Helper;
using WebFinalApi.Models.User;

namespace WebFinalApi.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(CommonDB dB) : base(dB)
        {
        }

        public IEnumerable<Users> GetAllUsers()
        {
            string sql = "SELECT * FROM users";
            return commonDB.Query<Users>(sql);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public UserDataOutput GetUser(int UserID)
        {
            var data = GetUserDataByUserID(UserID);
            var orderCount = GetOrderCount(UserID);
            return new UserDataOutput()
            {
                mobile = data.mobile,
                name = data.userName,
                needPayOrderCount = orderCount.Item1,
                waitRecive = orderCount.Item2
            };
        }

        /// <summary>
        /// 获取用户收货地址
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public AddressOutput GetAddress(int UserID)
        {
            AddressOutput addressOutput = new AddressOutput();
            var userAddress = GetUserAddress(UserID);
            if (userAddress != null)
            {
                addressOutput.userAddresses = userAddress.ToList();
            }
            return addressOutput;
        }


        public bool UserAddAddress(UserAddress userAddress, int UserID)
        {
            userAddress.userID = UserID;
           return AddAddress(userAddress ,UserID);
        }

        public bool UserUpdateAddress(UserAddress userAddress, int UserID)
        {
            userAddress.userID = UserID;
            return ChangeAddress(userAddress , UserID);
        }

        public bool UserDeleteAddress( int addressID, int UserID)
        {
            return DeleteAddress(addressID, UserID);
        }


        #region 帐号注册

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public LoginOutPut UserLogin(LoginModel login)
        {
            //验证码合法性验证
            int codeID = CodeVerification(login);
            //登录操作  （或注册）
            Users user = new Users()
            {
                mobile = login.mobile,
                ifdel = "0"
            };
            //查找该用户
            string sqlConditon = SelectSqlGenerate(user, new List<string>() { nameof(user.mobile), nameof(user.ifdel) });
            string sql = $"SELECT * FROM users {sqlConditon}";
            Users userData = commonDB.QueryFirstOrDefault<Users>(sql, user);
            if (userData == null)
            {
                //注册
                RegisterUser(user);
                //重新查找
                userData = commonDB.QueryFirstOrDefault<Users>(sql, user);
            }
            //失效验证码
            CodeVerificationInvalid(codeID);
            //转换UserID
            int id = Convert.ToInt32(userData.userId);
            string key = CodeVerificationHelper.GenerateLoginKey(id);
            return new LoginOutPut()
            {
                ifSuccess = true,
                key = key
            };
        }

        /// <summary>
        /// 验证码验证
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private int CodeVerification(LoginModel login)
        {
            CodeVerification codeSimple = new CodeVerification() { mobile = login.mobile, status = "0" };
            string sqlConditon = SelectSqlGenerate(codeSimple, new List<string>() { nameof(codeSimple.mobile), nameof(codeSimple.status) });
            //判断验证码是否合法
            int effective = Convert.ToInt32(GetSystemSet(0).value);
            string sql = $"SELECT * FROM code_verification {sqlConditon} AND DATEADD(MINUTE ,{effective} ,inittime) >= GETDATE() ORDER BY initTime DESC";
            CodeVerification code = commonDB.QueryFirstOrDefault<CodeVerification>(sql, codeSimple);
            if (code == null || string.IsNullOrWhiteSpace(code.value))
            {
                throw new VerificationException("验证码不存在，请先点击发送验证码.");
            }
            if (code.value.Equals(login.code))
            {
            }
            else
            {
                throw new VerificationException("验证码错误");
            }
            return code.id;
        }

        /// <summary>
        /// 标记验证码失效
        /// </summary>
        /// <param name="ID"></param>
        private void CodeVerificationInvalid(int ID)
        {
            CodeVerification simpleCode = new CodeVerification()
            {
                id = ID,
                status = "1"
            };
            //标记验证码已使用
            string sqlConditon = UpdateSqlGenerate(simpleCode, new List<string>() { nameof(simpleCode.status) }, new List<string>() { nameof(simpleCode.id) });
            string sql = $"UPDATE code_verification {sqlConditon}";
            commonDB.Excute(sql, simpleCode);
        }

        /// <summary>
        /// 用户注册操作
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RegisterUser(Users user)
        {
            user.inittime = DateTime.Now;
            string sqlConditon = InsertSqlGenerate(user, new List<string>()
            {
                nameof(user.userName),
                nameof(user.mobile),
                nameof(user.password),
                nameof(user.status),
                nameof(user.ifdel),
                nameof(user.inittime) });
            string sql = $@"INSERT INTO users {sqlConditon}";
            return commonDB.Excute(sql, user) == 1;
        }

        #endregion

        public bool UpdateUser(Users user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(string ID)
        {
            throw new NotImplementedException();
        }


    }
}