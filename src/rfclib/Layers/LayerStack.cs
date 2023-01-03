using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.Layers
{
    public class LayerStack
    {

        private List<Layer> stack;
        private int higestIndex = -1;

        public StackType StackType { get; private set; }

        public LayerStack()
        {
            stack = new List<Layer>();
            StackType = StackTypes.Unknown;
        }

        public void AddLayer(Layer layer)
        {
            layer.Index = ++higestIndex;
            stack.Add(layer);
            Order();
        }

        public void AddLayer(Layer layer, int index)
        {
            layer.Index = index;
            stack.Add(layer);
            Order();
        }

        public Layer GetLayer(int index)
        {
            return stack[index];
        }

        public Layer this[int index]
        {
            get => GetLayer(index);
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (Layer layer in stack)
            {
                sb.AppendLine(layer.ToString());
            }
            return sb.ToString();
        }

        private void Order()
        {
            stack = stack.OrderBy(x => x.Index).ToList();
            higestIndex = stack.Last().Index;
            UpdateStackType();
        }

        private void UpdateStackType()
        {
            StringBuilder sb = new();
            foreach (Layer layer in stack)
            {
                if (layer is Conductor)
                {
                    sb.Append('C');
                }
                else if (layer is Substrate)
                {
                    sb.Append('S');
                }
                else
                {
                    sb.Append('U');
                }
            }
            StackType = new StackType(sb.ToString());
        }

    }

    public static class LayerStacks
    {
        public static LayerStack CSC { get
            {
                LayerStack stack = new();
                stack.AddLayer(new Conductor("C1"));
                stack.AddLayer(new Substrate("S1"));
                stack.AddLayer(new Conductor("C2"));
                return stack;
            } 
        }

        public static LayerStack CS { get
            {
                LayerStack stack = new();
                stack.AddLayer(new Conductor("C1"));
                stack.AddLayer(new Substrate("S1"));
                return stack;
            }
}
    }
}
