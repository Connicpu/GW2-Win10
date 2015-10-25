using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2_Win10.API
{
    public class ApiKeyInfo : IApiType
    {
        public string Endpoint => "/v2/tokeninfo";

        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Permissions { get; set; } 
    }
}
