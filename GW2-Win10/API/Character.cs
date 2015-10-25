using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2_Win10.API
{
    public class Character : IApiType
    {
        public string Endpoint => "/v2/characters";

        // Fields for all APIs with `characters` scope
        public string Name { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public int Level { get; set; }
        public string Guild { get; set; }
        public double Age { get; set; }
        public long Deaths { get; set; }
        public List<CraftingSkill> Crafting { get; set; }

        // Present only with the `builds` scope
        public CharacterSpecializations Specializations { get; set; }

        // Present with the `inventories` or `builds` scope
        public List<EquipmentItem> Equipment { get; set; }

        // Present with the `inventories` scope
        public List<Bag> Bags { get; set; }
    }
}
