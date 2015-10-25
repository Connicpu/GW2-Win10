using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2_Win10.API
{
    public class Characters : List<string>, IApiType
    {
        public string Endpoint => "/v2/characters";
    }
}
