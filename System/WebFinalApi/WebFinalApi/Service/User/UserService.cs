using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Empty;

namespace WebFinalApi.Service
{
    public class UserService : BaseService, IUserService
    {
        CommonDB commonDB = new CommonDB();
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

        public bool RegisterUser(Users user)
        {
            string sqlConditon = GenerateInsertSql<Users>(user, new List<string>()
            {
                nameof(user.userName),
                nameof(user.mobile),
                nameof(user.password),
                nameof(user.status),
                nameof(user.ifdel),
                nameof(user.inittime) });
            string sql = $@"INSERT INTO users {sqlConditon}";
            return commonDB.Excute(sql) == 1;
        }

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