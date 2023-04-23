using System.Reflection;

namespace CCode.Roslyn
{
    public abstract partial class AttrbuteTemplate
    {
        public AttrbuteTemplate WithAdd(CustomAttributeData attributeData)
        {
            WithAdd(AttributeAnalysis.GetDefine(attributeData).View());
        }
    }
}
