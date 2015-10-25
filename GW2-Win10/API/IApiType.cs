using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2_Win10.API
{
    public interface IApiType
    {
        string Endpoint { get; }
    }

    public interface IApiListType
    {
        string Endpoint { get; }
    }
}
