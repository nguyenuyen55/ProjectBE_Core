using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.DTO
{
    public class ReturnReponse
    {
        public int MessageCode { get; set; }
        public string MessageReturn { get; set; }
    }
    public class AccountLoginRequestData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class AccountUpdateRefeshTokenRequestData
    {
        public int UserID { get; set; }
        public string RefeshToken { get; set; }

        public DateTime RefeshTokenExpired { get; set; }
    }
    public class ReturnResponData 
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string token { get; set; }
        public string refeshToken { get; set; }
    }
}