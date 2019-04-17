using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Empty;

namespace WebFinalApi.Service
{
    public interface IUserService 
    {
        Users GetUser(string ID);

        IEnumerable<Users> GetAllUsers();

        bool UpdateUser(Users user);

        bool DeleteUser(string ID);

        bool RegisterUser(Users user);
    }
}