
using rfclib.Layers;
using rfclib.TransmissionLines;

namespace RFCalc_WF
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool success = double.TryParse(textBox3.Text, out double epsr);
            success |= double.TryParse(textBox1.Text, out double d);
            //success |= double.TryParse(textBox2.Text, out double W);
            success |= double.TryParse(textBox4.Text, out double Z0);
            if (success)
            {
                LayerStack stack = LayerStacks.CSC;
                ((Substrate)stack[1]).Thickness = d;
                ((Substrate)stack[1]).RelativePermittivity = epsr;

                Microstrip m = Microstrip.CreateFromImpedance(stack, Z0);
                textBox2.Text = m.TrackWidth.ToString("0.000");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool success = double.TryParse(textBox3.Text, out double epsr);
            success |= double.TryParse(textBox1.Text, out double d);
            success |= double.TryParse(textBox2.Text, out double W);
            if (success)
            {
                LayerStack stack = LayerStacks.CSC;
                ((Substrate)stack[1]).Thickness = d;
                ((Substrate)stack[1]).RelativePermittivity = epsr;

                Microstrip m = Microstrip.CreateFromTrackWidth(stack, W);
                textBox4.Text = m.Impedance.ToString("0.000");
            }
        }
    }
}