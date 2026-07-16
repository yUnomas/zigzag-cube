#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EssentialKit.Common.Android;

namespace VoxelBusters.EssentialKit.ExtrasCore.Android
{
    public class NativeRequestInfoForAgeComplianceOptions : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion
        #region Constructor

        // Default constructor
        public NativeRequestInfoForAgeComplianceOptions(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }
        public NativeRequestInfoForAgeComplianceOptions(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }
        public NativeRequestInfoForAgeComplianceOptions() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG_ENABLED
        ~NativeRequestInfoForAgeComplianceOptions()
        {
            DebugLogger.Log("Disposing NativeRequestInfoForAgeComplianceOptions");
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

        public void AddContentAgeGateRange(int lowerBoundInYears, int upperBoundInYears)
        {
            Call(Native.Method.kAddContentAgeGateRange, lowerBoundInYears, upperBoundInYears);
        }
        public NativeList<NativeAgeRange> GetAvailableContentAgeGates()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetAvailableContentAgeGates);
            NativeList<NativeAgeRange> data  = new  NativeList<NativeAgeRange>(nativeObj);
            return data;
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.essentialkit.extras.RequestInfoForAgeComplianceOptions";

            internal class Method
            {
                internal const string kAddContentAgeGateRange = "addContentAgeGateRange";
                internal const string kGetAvailableContentAgeGates = "getAvailableContentAgeGates";
            }

        }
    }
}
#endif