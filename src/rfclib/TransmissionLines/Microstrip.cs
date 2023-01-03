using rfclib.Layers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.TransmissionLines
{
    public class Microstrip : ITransmissionLine
    {
        // Literature:
        // [1] David M. Pozar, MICROWAVE ENGINEERING. Fourth Edition; Wiley&Sons, 2012.

        private LayerStack layerStack;                     // Layer stack of the microstrip line

        public double Impedance { get; set; }              // Impedance of the microstrip line in Ohms
        public double TrackWidth { get; set; }             // Width of the microstrip [m]

        public LayerStack LayerStack
        {
            get => layerStack;
            set
            {
                if (value.StackType != StackTypes.CSC)
                {
                    throw new ArgumentException("Microstrip line can only be constructed from a CSC stack.");
                }
                else
                {
                    layerStack = value;
                }
            }
        }

        private Microstrip(LayerStack stack)
        {
            layerStack = stack;
        }

        public static Microstrip CreateFromImpedance(LayerStack stack, double Z0)
        {
            Microstrip m = new(stack)
            {
                Impedance = Z0
            };
            m.CalculateTrackWidth();
            return m;
        }

        public static Microstrip CreateFromTrackWidth(LayerStack stack, double W)
        {
            Microstrip m = new(stack)
            {
                TrackWidth = W
            };
            m.CalculateImpedance();
            return m;
        }

        private void CalculateTrackWidth()
        {
            Substrate s = (Substrate)layerStack.GetLayer(1);
            TrackWidth = CalculateTrackWidth(s.RelativePermittivity,s.Thickness, Impedance);
        }
        
        private void CalculateImpedance()
        {
            Substrate s = (Substrate)layerStack.GetLayer(1);
            Impedance = CalculateImpedance(s.RelativePermittivity, s.Thickness, TrackWidth);
        }
        
        private static double CalculateImpedance(double dielectricConstant, double substrateThickness, double trackWidth)
        {
            // According to eq. 3.196 in [1]

            double effectiveDielectricConstant = CalculateEffectiveDielectricConstant(dielectricConstant, substrateThickness, trackWidth);
            double z01 = 60.0 / Math.Sqrt(effectiveDielectricConstant) * Math.Log(((8 * substrateThickness) / trackWidth) + trackWidth / (4 * substrateThickness));
            double z02 = 120.0 * Math.PI / (Math.Sqrt(effectiveDielectricConstant) * (trackWidth / substrateThickness + 1.393 + 0.667 * Math.Log(trackWidth / substrateThickness + 1.444)));

            return trackWidth / substrateThickness <= 1 ? z01 : z02;
        }

        private static double CalculateTrackWidth(double dielectricConstant, double substrateThickness, double impedance)
        {
            // According to eq. 3.197 in [1]
            double a = impedance / 60 * Math.Sqrt((dielectricConstant + 1) / 2) + (dielectricConstant - 1) / (dielectricConstant + 1) * (0.23 + 0.11 / dielectricConstant);
            double b = (377 * Math.PI) / (2 * impedance * Math.Sqrt(dielectricConstant));
            double wd1 = (8 * Math.Exp(a)) / (Math.Exp(2 * a) - 2);
            double wd2 = 2 / Math.PI * (b - 1 - Math.Log(2 * b - 1) + (dielectricConstant - 1) / (2 * dielectricConstant) * (Math.Log(b - 1) + 0.39 - 0.61 / dielectricConstant));
            double wd = (wd1 + wd2) / 2;

            return wd < 2 ? wd1 * substrateThickness : wd2 * substrateThickness;
        }

        private static double CalculateEffectiveDielectricConstant(double dielectricConstant, double substrateThickness, double trackWidth)
        {
            // According to eq. 3.195 in [1]
            return (dielectricConstant + 1) / 2 + (dielectricConstant - 1) / 2 / Math.Sqrt(1 + 12 * substrateThickness / trackWidth);
        }
    }
}
