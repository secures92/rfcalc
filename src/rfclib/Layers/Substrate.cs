using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.Layers
{
    public class Substrate : Layer
    {
        public double RelativePermittivity { get; set; }
        public double RelativePermeability { get; set; }
        public double DissipationFactor { get; set; }
        public double Thickness { get; set; }    

        public Substrate(string name)
        {
            Name = name;
            RelativePermittivity = 1.0;
            RelativePermeability = 1.0;
            DissipationFactor = 0.0;
            Thickness = 0.0;
        }
    }
}
