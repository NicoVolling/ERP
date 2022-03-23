using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BaseLib.Helpers
{
    public static class ReflectionHelper
    {
        public static bool DoesInheritFrom(Type OrigingType, Type BaseType) 
        {
            if(OrigingType.BaseType == null) 
            {
                return false;
            }
            if(OrigingType.BaseType == BaseType) 
            {
                return true;
            }
            return DoesInheritFrom(OrigingType.BaseType, BaseType);
        }
    }
}
