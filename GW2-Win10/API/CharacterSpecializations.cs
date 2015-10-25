using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2_Win10.API
{
    public class CharacterSpecializations
    {
        public List<Specialization> PvE { get; set; }
        public List<Specialization> PvP { get; set; }
        public List<Specialization> WvW { get; set; }
    }
}
