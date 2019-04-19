using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;

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

        public bool VerificationUser(Users user)
        {
            //手机号码验证是否重复
            string sqlConditon = SelectSqlGenerate(user, new List<string>()
            {
                nameof(user.mobile),
                nameof(user.ifdel)
            });
            string sql = $"SELECT * FROM users {sqlConditon}";
            Users existData = commonDB.QueryFirstOrDefault<Users>(sql,user);
            if (existData!=null)
            {
                throw new VerificationException("手机号码已注册.");
            }
            return true;
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