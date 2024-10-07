using UnityEngine;

namespace com.cyborgAssets.internalIBPEditor
{
    public static class Vector4Extensions
    {
        public static Quaternion ToQuaternion (this Vector4 vector4)
        {
            return new Quaternion (vector4.x, vector4.y, vector4.z, vector4.w);
        }        
    }
}