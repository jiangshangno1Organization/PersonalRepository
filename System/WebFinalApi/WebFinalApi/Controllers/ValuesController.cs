using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFinalApi.Filter;
using WebFinalApi.Service;

namespace WebFinalApi.Controllers
{
    public class Users
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string UserEmail { set; get; }
    }

    public class ValuesController : ApiController
    {
        private static List<Users> _userList;
        static ValuesController()
        {
            _userList = new List<Users>
            {
                new Users {UserID = 1, UserName = "zzl", UserEmail = "bfyxzls@sina.com"},
                new Users {UserID = 2, UserName = "Spiderman", UserEmail = "Spiderman@cnblogs.com"},
                new Users {UserID = 3, UserName = "Batman", UserEmail = "Batman@cnblogs.com"}
            };
        }
        /// <summary>
        /// User Data List
        /// </summary>

        /// <summary>
        /// 得到列表对象
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Users> Get()
        {
            return _userList;
        }

        /// <summary>
        /// 得到一个实体，根据主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users Get(int id)
        {
            SystemService system = new SystemService();
            DateTime dt = system.GetSystemTime();
            var dd = system.GetT<dynamic>();
            return _userList.FirstOrDefault();// (i => i.UserID == id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单对象，它是唯一的</param>
        /// <returns></returns>
        [AuthorizationFilter]
        public Users Post([FromBody] Users entity)
        {
            entity.UserID = _userList.Max(x => x.UserID) + 1;
            _userList.Add(entity);
            return entity;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="form">表单对象，它是唯一的</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody]Users entity)
        {
            var user = _userList.FirstOrDefault(i => i.UserID == id);
            if (user != null)
            {
                user.UserName = entity.UserName;
                user.UserEmail = entity.UserEmail;
            }
            else
            {

                _userList.Add(entity);
            }
            return Json(user);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public void Delete(int id)
        {
            //_userList.Remove(_userList.FirstOrDefault(i => i.UserID == id));
            _userList.Remove(_userList.FirstOrDefault());
        }
        public string Options()
        {
            return null; // HTTP 200 response with empty body
        }

    }
}
