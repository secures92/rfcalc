using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.TransmissionLines.Microstrips
{
    public interface IMicrostripVariant
    {
        public double CalculateImpedance(double dielectricConstant, double substrateThickness, double trackWidth);
        public double CalculateTrackWidth(double dielectricConstant, double substrateThickness, double impedance);
        //public double CalculateEffectiveDielectricConstant(double dielectricConstant, double substrateThickness, double trackWidth);

    }
}
