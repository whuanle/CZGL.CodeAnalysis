using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Reflect.Units
{
    public struct GenericeConstraintInfo
    {
        public GenericeConstraintInfo(GenericKeyword a, ConstraintLocation b, string c)
        {
            Keyword = a;
            Location = b;
            Value = c;
        }

        public readonly GenericKeyword Keyword;
        public readonly ConstraintLocation Location;
        public readonly string Value;
    }
}
