using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rfclib.Layers
{
    public class StackType
    {
        private readonly string type = "";

        public StackType()
        {
            type = "U";
        }

        public StackType(string type)
        {
            this.type = type.ToUpper();
        }

        public override string ToString()
        {
            return type;
        }

        public override bool Equals(object? obj)
        {
            return obj != null && type.Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return type.GetHashCode();
        }

        public static bool operator ==(StackType left, StackType right)
        {
            return left.type.Equals(right.type);
        }

        public static bool operator !=(StackType left, StackType right)
        {
            return !left.type.Equals(right.type);
        }


    }

    public static class StackTypes
    {
        public static readonly StackType Unknown = new("U");
        public static readonly StackType CS = new("CS");
        public static readonly StackType CSC = new("CSC");
    }
}
