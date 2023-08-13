using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Services.Identity
{
    public interface ITokenProvider
    {
        string AuthAccessToken { get; set; }
    }
}
