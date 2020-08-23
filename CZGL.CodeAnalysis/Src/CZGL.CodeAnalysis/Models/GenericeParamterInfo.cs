using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Models
{
    public class GenericeParamterInfo : GenericeParamter
    {
        public bool IsHasefineGenerice { get; set; }

        public bool IsConstraint { get; set; }

        public GenericeConstraint[] Constraints { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public Type ParamterType { get; set; }
    }
}
