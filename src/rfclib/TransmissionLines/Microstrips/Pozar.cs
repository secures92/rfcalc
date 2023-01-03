namespace rfclib.TransmissionLines.Microstrips
{
    public class Pozar : IMicrostripVariant
    {
        // Literature:
        // [1] David M. Pozar, MICROWAVE ENGINEERING. Fourth Edition; Wiley&Sons, 2012.

        #region Singleton pattern
        private static readonly Lazy<Pozar> lazy = new(() => new Pozar());

        public static Pozar Instance { get { return lazy.Value; } }

        private Pozar() { }
        #endregion

        #region IMicrostripVariant implementation
        public double CalculateImpedance(double dielectricConstant, double substrateThickness, double trackWidth)
        {
            // According to eq. 3.196 in [1]

            double effectiveDielectricConstant = CalculateEffectiveDielectricConstant(dielectricConstant, substrateThickness, trackWidth);
            double z01 = 60.0 / Math.Sqrt(effectiveDielectricConstant) * Math.Log(8 * substrateThickness / trackWidth + trackWidth / (4 * substrateThickness));
            double z02 = 120.0 * Math.PI / (Math.Sqrt(effectiveDielectricConstant) * (trackWidth / substrateThickness + 1.393 + 0.667 * Math.Log(trackWidth / substrateThickness + 1.444)));

            return trackWidth / substrateThickness <= 1 ? z01 : z02;
        }

        public double CalculateTrackWidth(double dielectricConstant, double substrateThickness, double impedance)
        {
            // According to eq. 3.197 in [1]
            double a = impedance / 60 * Math.Sqrt((dielectricConstant + 1) / 2) + (dielectricConstant - 1) / (dielectricConstant + 1) * (0.23 + 0.11 / dielectricConstant);
            double b = 377 * Math.PI / (2 * impedance * Math.Sqrt(dielectricConstant));
            double wd1 = 8 * Math.Exp(a) / (Math.Exp(2 * a) - 2);
            double wd2 = 2 / Math.PI * (b - 1 - Math.Log(2 * b - 1) + (dielectricConstant - 1) / (2 * dielectricConstant) * (Math.Log(b - 1) + 0.39 - 0.61 / dielectricConstant));
            double wd = (wd1 + wd2) / 2;

            return wd < 2 ? wd1 * substrateThickness : wd2 * substrateThickness;
        }

        public double CalculateEffectiveDielectricConstant(double dielectricConstant, double substrateThickness, double trackWidth)
        {
            // According to eq. 3.195 in [1]
            return (dielectricConstant + 1) / 2 + (dielectricConstant - 1) / 2 / Math.Sqrt(1 + 12 * substrateThickness / trackWidth);
        }
        #endregion
    }
}
