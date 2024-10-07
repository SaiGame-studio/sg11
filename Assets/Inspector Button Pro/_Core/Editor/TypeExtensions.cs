using System;

namespace com.cyborgAssets.internalIBPEditor
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue (this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType (t) == null)
                return Activator.CreateInstance (t);
            
            return null;
        }
    }
}