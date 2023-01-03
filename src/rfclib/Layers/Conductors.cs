using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.Layers
{
    public static class Conductors
    {
        // Source: http://hyperphysics.phy-astr.gsu.edu/hbase/Tables/rstiv.html
        public static readonly Conductor PEC = new("PEC") { Resistivity = 0 };
        public static readonly Conductor Copper = new("Copper") { Resistivity = 1.72e-8 };
        public static readonly Conductor Aluminum = new("Aluminum") { Resistivity = 2.653e-8 };
        public static readonly Conductor Silver = new("Silver") { Resistivity = 1.59e-8 };
    }
}
