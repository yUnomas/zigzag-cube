using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EssentialKit.ExtrasCore
{
    public interface INativeUtilityInterface : INativeFeatureInterface
    {
        #region Methods

        void OpenAppStorePage(string applicationId);

        void OpenApplicationSettings();
        
        void RequestInfoForAgeCompliance(RequestInfoForAgeComplianceOptions options, EventCallback<InfoForAgeCompliance> onComplete, AgeComplianceMockData mockData = null);

        #endregion
    }
}