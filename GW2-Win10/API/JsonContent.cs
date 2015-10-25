using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2_Win10.API
{
    class JsonContent : StringContent
    {
        public JsonContent(object data)
            : base(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(JToken jdata)
            : base(jdata.ToString(Formatting.None), Encoding.UTF8, "application/json")
        {
        }
    }
}
