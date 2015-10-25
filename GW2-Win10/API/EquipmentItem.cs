using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2_Win10.API
{
    public class EquipmentItem
    {
        public int Id { get; set; }
        public string Slot { get; set; }
        public List<int> Upgrades { get; set; }
        public List<int> Infusions { get; set; }
        public int? Skin { get; set; }
    }
}
