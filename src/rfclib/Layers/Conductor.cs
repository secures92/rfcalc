using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.Layers
{
    public class Conductor : Layer
    {
        public double Resistivity { get; set; }
        public double Roughness { get; set; }
        public double Thickness { get; set; }
        public double RelativePermeability { get; set; }

        public Conductor(string name)
        {
            Name = name;
            Resistivity = 0;
            Roughness = 0;
            Thickness = 0;
            RelativePermeability = 1;
        }
    }
}
