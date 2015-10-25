using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2_Win10.API
{
    public class BankItem : IApiListType
    {
        public string Endpoint => "/v2/account/bank";

        public int Id { get; set; }
        public int Count { get; set; }
        public int? Skin { get; set; }
        public List<int> Upgrades { get; set; }
        public List<int> Infusions { get; set; }
    }
}
