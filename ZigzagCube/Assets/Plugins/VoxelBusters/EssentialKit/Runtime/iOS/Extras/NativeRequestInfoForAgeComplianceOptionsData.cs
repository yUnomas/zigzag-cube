#if UNITY_IOS || UNITY_TVOS
using System.Runtime.InteropServices;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EssentialKit.ExtrasCore.iOS
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeRequestInfoForAgeComplianceOptionsData
    {
        #region Properties

        public NativeArray AvailableContentAgeGates
        {
            get;
            internal set;
        }

        #endregion

        #region Public methods

        public void Dispose()
        {
            GCHandle handle = GCHandle.FromIntPtr(AvailableContentAgeGates.Pointer);
            handle.Free();
        }

        #endregion
    }
}
#endif