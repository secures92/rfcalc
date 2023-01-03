using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.Layers
{
    public abstract class Layer
    {
        private int index = 0;

        private string name = "Unknown";

        public string Name { get => name; protected set => name = value; }
        public int Index { get => index; set => index = value; }

        public override string ToString()
        {
            return name;
        }
    }
}
