using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EssentialKit
{
    /// <summary>
    /// Represents the utility unity settings.
    /// </summary>
    [Serializable]
    public class UtilityUnitySettings : SettingsPropertyGroup
    {

        #region Fields

        [SerializeField]
        [Tooltip("If set to true, the age compliance utility api's will be configured correctly.")]
        private bool m_usesAgeComplianceApi = false;

        #endregion


        #region Properties

        /// <summary>
        /// Get's the status of usage of age compliance api. Enable this in inspector settings if you want to use age compliance api's.
        /// </summary>
        public bool UsesAgeComplianceApi => m_usesAgeComplianceApi;

        #endregion

        #region Constructors

        public UtilityUnitySettings(bool isEnabled = true, bool usesAgeComplianceApi = false)
            : base(isEnabled: isEnabled, name: NativeFeatureType.kExtras)
        {
            m_usesAgeComplianceApi = usesAgeComplianceApi;
        }

        #endregion
    }
}