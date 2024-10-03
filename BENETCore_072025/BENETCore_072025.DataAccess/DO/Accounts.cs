using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.DO
{
    public class Accounts
    {
        [Key]
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public string? RefeshToken { get; set; }

        public DateTime? RefeshTokenExpired { get; set; }
    }
}
