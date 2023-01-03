using rfclib.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.TransmissionLines
{
    public interface ITransmissionLine
    {
        public double Impedance { get; set; }
        public LayerStack LayerStack { get; set; }
    }
}
