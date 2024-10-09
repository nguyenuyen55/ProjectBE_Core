using BENETCore_072025.DataAccess.DBContext;
using BENETCore_072025.DataAccess.DO;
using BENETCore_072025.DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.Services
{
    public class AccountService : IAccountService
    {
        private DBHotelContext _hotelDbContext;

        public AccountService(DBHotelContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> AccountUpdateRefeshToken(AccountUpdateRefeshTokenRequestData requestData)
        {
            try
            {
                var user = _hotelDbContext.accounts.Where(s => s.ID == requestData.UserID).FirstOrDefault();
                if (user != null)
                {
                    user.RefeshToken = requestData.RefeshToken;
                    user.RefeshTokenExpired = requestData.RefeshTokenExpired;
                    _hotelDbContext.accounts.Update(user);
                    _hotelDbContext.SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return 0;
        }

        public async Task<Function> GetFunctionByCode(string code)
        {
            return _hotelDbContext.function.ToList().Where(f => f.FunctionCode == code).FirstOrDefault();
        }

        public async Task<UserPermission> GetPermissionByUserID(int userID, int functionID)
        {
            return _hotelDbContext.userPermission.ToList().Where(f => f.UserID == userID&&f.FunctionID==functionID).FirstOrDefault();

        }

        public async Task<Accounts> UserLogin(AccountLoginRequestData requestData)
        {
            try
            {
                var user_db = _hotelDbContext.accounts.Where(s => s.UserName == requestData.UserName && s.Password == requestData.Password).FirstOrDefault();
                if (user_db == null)
                {
                    return user_db;
                }
                return user_db;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
