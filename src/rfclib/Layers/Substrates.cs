using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.Layers
{
    public static class Substrates
    {
        // Source: https://www.microwaves101.com/encyclopedias/soft-substrate-materials
        public static readonly Substrate Air = new("Air") { RelativePermittivity = 1.0, DissipationFactor = 0 };
        public static readonly Substrate FR4 = new("FR4") { RelativePermittivity = 4.8, DissipationFactor = 0.022 };
        public static readonly Substrate RO4350 = new("RO4350") { RelativePermittivity = 3.48, DissipationFactor = 0.004 };
    }
}
