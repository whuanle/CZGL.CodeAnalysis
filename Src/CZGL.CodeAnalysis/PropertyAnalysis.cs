using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CZGL.CodeAnalysis
{
    public class PropertyAnalysis
    {
        private PropertyDescriptor descriptor;
        private PropertyInfo _propertyInfo;
        public PropertyAnalysis(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public string GetVisibility()
        {
            MethodInfo method = _propertyInfo.GetGetMethod();
            return
                method.IsPublic ? "public" :
                method.IsPrivate ? "private" :
                method.IsAssembly ? "internal" :
                method.IsFamily ? "protected" :
                method.IsFamilyOrAssembly ? "protected internal" :
                null;
        }
    }
}
