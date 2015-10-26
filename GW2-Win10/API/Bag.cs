using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GW2_Win10.API
{
    public class Bag
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public List<BagItem> Inventory { get; set; }

        [JsonIgnore]
        public IEnumerable<BagItem> NonEmptyItems => Inventory.Where(item => item != null);
    }
}
