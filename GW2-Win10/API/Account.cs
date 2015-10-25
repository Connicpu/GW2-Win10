using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;

namespace GW2_Win10.API
{
    public class Account : IApiType
    {
        public string Endpoint => "/v2/account";

        public string Id { get; set; }
        public string Name { get; set; }
        public int World { get; set; }
        public List<string> Guilds { get; set; }
        public DateTime Created { get; set; }
    }
}
