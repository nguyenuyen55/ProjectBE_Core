using BENETCore_072025.DataAccess.DO;
using BENETCore_072025.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.Services
{
    public interface IAccountService
    {
        Task<Accounts> UserLogin(AccountLoginRequestData requestData);

        Task<int> AccountUpdateRefeshToken(AccountUpdateRefeshTokenRequestData requestData);
    
        Task<Function> GetFunctionByCode(string code);
        Task<UserPermission> GetPermissionByUserID(int userID, int functionID);
    }
}
