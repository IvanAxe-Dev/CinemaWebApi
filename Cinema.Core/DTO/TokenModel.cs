using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.DTO
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set;}
    }
}
