#if UNITY_IOS || UNITY_TVOS
using System;
using System.Runtime.InteropServices;

namespace VoxelBusters.EssentialKit.ExtrasCore.iOS
{
    internal static class ExtrasBinding
    {
        [DllImport("__Internal")]
        public static extern void NPExtrasRequestInfoForAgeCompliance(ref NativeRequestInfoForAgeComplianceOptionsData options, ref NativeAgeComplianceMockData mockData, IntPtr tagPtr, RequestInfoForAgeComplianceNativeCallback callback);
    }
}
#endif