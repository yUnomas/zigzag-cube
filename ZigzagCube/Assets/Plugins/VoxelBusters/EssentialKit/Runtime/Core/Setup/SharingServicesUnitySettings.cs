using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EssentialKit
{
    /// <summary>
    /// Represents the sharing services unity settings.
    /// </summary>
    [Serializable]
    public partial class SharingServicesUnitySettings : SettingsPropertyGroup
    {
        #region Constructors

        public SharingServicesUnitySettings(bool isEnabled = true)
            : base(isEnabled: isEnabled, name: NativeFeatureType.kSharingServices)
        { }

        #endregion
    }
}