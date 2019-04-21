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
        CommonDB commonDB ;
        public UserService(CommonDB DB)
        {
            commonDB = DB;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            string sql = "SELECT * FROM users";
            return commonDB.Query<Users>(sql);
        }

        public Users GetUser(string ID)
        {
            string sql = "SELECT * FROM users WHERE userid = @id";
            return commonDB.QueryFirstOrDefault<Users>(sql, new { id = ID });
        }

        #region 帐号注册

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public LoginOutPut UserLogin(LoginModel login)
        {
            CodeVerification codeSimple = new CodeVerification() { mobile = login.mobile, status = "0" };
            string sqlConditon = SelectSqlGenerate(codeSimple, new List<string>() { nameof(codeSimple.mobile), nameof(codeSimple.status) });
            //判断验证码是否合法
            string sql = $"SELECT * FROM code_verification {sqlConditon} ORDER BY initTime DESC";
            CodeVerification code = commonDB.QueryFirstOrDefault<CodeVerification>(sql, codeSimple);
            if (code == null || string.IsNullOrWhiteSpace(code.value))
            {
                throw new VerificationException("验证码不存在，请先点击发送验证码");
            }
            if (code.value.Equals(login.code))
            {
                //登录操作  （或注册）
                Users user = new Users()
                {
                    mobile = login.mobile,
                    ifdel = "0"
                };
                //查找该用户
                sqlConditon = SelectSqlGenerate(user, new List<string>() { nameof(user.mobile), nameof(user.ifdel) });
                sql = $"SELECT * FROM users {sqlConditon}";
                Users userData = commonDB.QueryFirstOrDefault<Users>(sql, user);
                if (userData == null)
                {
                    //注册
                    RegisterUser(user);

                    //重新查找
                    userData = commonDB.QueryFirstOrDefault<Users>(sql, user);
                }
                int id = Convert.ToInt32(userData.userId);
                CodeVerificationHelper codeVerification = new CodeVerificationHelper(); 
                string key = codeVerification.GenerateLoginKey(id);
                return new LoginOutPut()
                {
                    ifSuccess = true,
                    key = key
                };
            }
            else
            {
                throw new VerificationException("验证码错误");
            }
        }

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