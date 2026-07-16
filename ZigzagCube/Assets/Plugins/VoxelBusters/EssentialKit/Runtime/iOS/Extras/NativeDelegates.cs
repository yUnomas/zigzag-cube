#if UNITY_IOS || UNITY_TVOS
using System;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EssentialKit.ExtrasCore.iOS
{
    internal delegate void RequestInfoForAgeComplianceNativeCallback(NativeInfoForAgeComplianceData nativeData, NativeError nativeError, IntPtr tagPtr);

}
#endif