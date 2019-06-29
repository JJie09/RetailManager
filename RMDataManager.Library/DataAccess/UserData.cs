using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class UserData
    {
        public UserModel GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var p = new { Id = Id };
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "RMData");
            if (output.Count > 0)
                return output.First();

            return null;
        }
        //public List<UserModel> RegisterUser(String id, String firstName, String lastName, String emailAddress)
        public void RegisterUser(UserModel user)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var p = new { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, EmailAddress = user.EmailAddress };
            sql.SaveData<object>("dbo.spUserRegister", p, "RMData");
        }
    }
}
