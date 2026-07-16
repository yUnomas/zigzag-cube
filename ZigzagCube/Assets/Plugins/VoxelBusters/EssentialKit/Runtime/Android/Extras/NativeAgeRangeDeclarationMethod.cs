#if UNITY_ANDROID
using UnityEngine;
using System.Collections.Generic;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EssentialKit.ExtrasCore.Android
{
    public enum NativeAgeRangeDeclarationMethod
    {
        NotDeclared = 0,
        DeclaredBySelf = 1,
        DeclaredWithPayment = 2,
        DeclaredWithValidId = 3,
        DeclaredWithOther = 4,
        DeclaredByGuardian = 5,
        DeclaredByGuardianWithPayment = 6,
        DeclaredByGuardianWithValidId = 7,
        DeclaredByGuardianWithOther = 8,
        Unknown = 9,
        NotApplicable = 10
    }
    public class NativeAgeRangeDeclarationMethodHelper
    {
        internal const string kClassName = "com.voxelbusters.essentialkit.extras.AgeRangeDeclarationMethod";

        public static AndroidJavaObject CreateWithValue(NativeAgeRangeDeclarationMethod value)
        {
#if NATIVE_PLUGINS_DEBUG_ENABLED
            DebugLogger.Log("[NativeAgeRangeDeclarationMethodHelper : NativeAgeRangeDeclarationMethodHelper][Method(CreateWithValue) : NativeAgeRangeDeclarationMethod]");
#endif
            AndroidJavaClass javaClass = new AndroidJavaClass(kClassName);
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            return values[(int)value];
        }

        public static NativeAgeRangeDeclarationMethod ReadFromValue(AndroidJavaObject value)
        {
            return (NativeAgeRangeDeclarationMethod)value.Call<int>("ordinal");
        }
    }
}
#endif