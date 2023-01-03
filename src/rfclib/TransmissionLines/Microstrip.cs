using rfclib.Layers;
using rfclib.TransmissionLines.Microstrips;


namespace rfclib.TransmissionLines
{
    public class Microstrip : ITransmissionLine
    {
        private readonly IMicrostripVariant variant = Pozar.Instance;

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
            var m = new Microstrip(stack) { Impedance = Z0 };
            m.CalculateTrackWidth();
            return m;
        }

        public static Microstrip CreateFromTrackWidth(LayerStack stack, double W)
        {
            var m = new Microstrip(stack) { TrackWidth = W };
            m.CalculateImpedance();
            return m;
        }

        private void CalculateTrackWidth()
        {
            Substrate s = (Substrate)layerStack[1];
            TrackWidth = variant.CalculateTrackWidth(s.RelativePermittivity, s.Thickness, Impedance);
        }

        private void CalculateImpedance()
        {
            Substrate s = (Substrate)layerStack[1];
            Impedance = variant.CalculateImpedance(s.RelativePermittivity, s.Thickness, TrackWidth);
        }


    }
}
