#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EssentialKit.Common.Android;

namespace VoxelBusters.EssentialKit.ExtrasCore.Android
{
    public class NativeAgeComplianceMockData : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion
        #region Constructor

        // Default constructor
        public NativeAgeComplianceMockData(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }
        public NativeAgeComplianceMockData(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }
        public NativeAgeComplianceMockData() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG_ENABLED
        ~NativeAgeComplianceMockData()
        {
            DebugLogger.Log("Disposing NativeAgeComplianceMockData");
        }
#endif
        #endregion
        #region Static methods
        private static AndroidJavaClass GetClass()
        {
            if (m_nativeClass == null)
            {
                m_nativeClass = new AndroidJavaClass(Native.kClassName);
            }
            return m_nativeClass;
        }

        #endregion
        #region Public methods

        public NativeAgeRange GetAgeRange()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetAgeRange);
            NativeAgeRange data  = new  NativeAgeRange(nativeObj);
            return data;
        }
        public NativeAgeRangeDeclarationMethod GetAgeRangeDeclarationMethod()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetAgeRangeDeclarationMethod);
            NativeAgeRangeDeclarationMethod data  = NativeAgeRangeDeclarationMethodHelper.ReadFromValue(nativeObj);
            return data;
        }
        public void SetAgeRange(int lowerBound, int upperBound)
        {
            Call(Native.Method.kSetAgeRange, lowerBound, upperBound);
        }
        public void SetAgeRangeDeclarationMethod(int declarationMethod)
        {
            Call(Native.Method.kSetAgeRangeDeclarationMethod, declarationMethod);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.essentialkit.extras.AgeComplianceMockData";

            internal class Method
            {
                internal const string kSetAgeRange = "setAgeRange";
                internal const string kGetAgeRange = "getAgeRange";
                internal const string kGetAgeRangeDeclarationMethod = "getAgeRangeDeclarationMethod";
                internal const string kSetAgeRangeDeclarationMethod = "setAgeRangeDeclarationMethod";
            }

        }
    }
}
#endif