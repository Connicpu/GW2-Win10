using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GW2_Win10.API
{
    public interface IApiType
    {
        [JsonIgnore]
        string Endpoint { get; }
    }

    public interface IApiListType
    {
        [JsonIgnore]
        string Endpoint { get; }
    }
}
