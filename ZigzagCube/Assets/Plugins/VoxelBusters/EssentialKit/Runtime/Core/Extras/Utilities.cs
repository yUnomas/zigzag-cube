using System;
using System.Collections;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EssentialKit.ExtrasCore;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EssentialKit
{
    /** @defgroup Utilities Utilities
    *   @brief Provides cross-platform interface to access commonly used native features.
    */
    /// <summary>
    /// Provides a cross-platform interface to access commonly used native features.
    /// </summary>
    /// @ingroup Utilities
    public static partial class Utilities
    {
        #region Static fields

        [ClearOnReload]
        private static INativeUtilityInterface s_nativeInterface;

        [ClearOnReload]
        private static UtilityUnitySettings s_settings;

        #endregion

        #region Static methods

        /// @name Advanced Usage
        /// @{

        /// <summary>
        /// Initializes the utilities module with the given settings. This call is optional and only need to be called if you have custom settings to initialize this feature.
        /// </summary>
        /// <param name="settings">The settings to be used for initialization.</param>
        /// <remarks>
        /// The settings configure the utilities module.
        /// </remarks>
        public static void Initialize(UtilityUnitySettings settings)
        {
            // Configure interface
            s_nativeInterface = NativeFeatureActivator.CreateInterface<INativeUtilityInterface>(ImplementationSchema.Extras, true);
            s_settings = settings;
        }
        /// @}

        /// <summary>
        /// Opens the app store website page associated with this app.
        /// </summary>
        public static void OpenAppStorePage()
        {
            // validate argument
            var settings = EssentialKitSettings.Instance.ApplicationSettings;
            string appId = settings.GetAppStoreIdForActiveOrSimulationPlatform();
            OpenAppStorePage(appId);
        }


        /// <summary>
        /// Opens the app store page associated with the specified application id.
        /// </summary>
        /// <description>
        /// For iOS platform, id is the value that identifies your app on App Store. 
        /// And on Android, it will be same as app's bundle identifier (com.example.test).
        /// </description>
        /// <param name="applicationIds">An array of string values, that holds app id's of each supported platform.</param>
        /// <example>
        /// The following code example shows how to open store link.
        /// <code>
        /// using UnityEngine;
        /// using System.Collections;
        /// using VoxelBusters.EssentialKit;
        /// 
        /// public class ExampleClass : MonoBehaviour 
        /// {
        ///     public void OpenStorePage ()
        ///     {
        ///         Utilities.OpenStoreLink(PlatformValue.Android("com.example.app"), PlatformValue.IOS("ios-app-id"));
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void OpenAppStorePage(params RuntimePlatformConstant[] applicationIds)
        {
            // validate arguments
            Assert.IsNotNullOrEmpty(applicationIds, "applicationIds");

            try
            {
                var targetValue = RuntimePlatformConstantUtility.FindConstantForActivePlatform(applicationIds);
                if (targetValue == null)
                {
                    DebugLogger.LogWarning(EssentialKitDomain.Default, "Application id not found for current platform.");
                    return;
                }

                s_nativeInterface.OpenAppStorePage(targetValue.Value);
            }
            catch (Exception exception)
            {
                DebugLogger.LogException(EssentialKitDomain.Default, exception);
            }
        }

        /// <summary>
        /// Opens the app store website page associated with the specified application id.
        /// </summary>
        /// <param name="applicationId">Application id.</param>
        public static void OpenAppStorePage(string applicationId)
        {
            // validate arguments
            Assert.IsNotNullOrEmpty(applicationId, "Application id null/empty.");

            try
            {
                s_nativeInterface.OpenAppStorePage(applicationId);
            }
            catch (Exception exception)
            {
                DebugLogger.LogException(EssentialKitDomain.Default, exception);
            }
        }

        /// <summary>
        /// Opens the app settings page associated with this app.
        /// </summary>
        /// <description>
        /// For iOS platform, this will open the settings app to the app's custom settings page.
        /// On Android, this will open app's settings page in the device's settings app.
        /// </description>
        public static void OpenApplicationSettings()
        {
            try
            {
                s_nativeInterface.OpenApplicationSettings();
            }
            catch (Exception exception)
            {
                DebugLogger.LogException(EssentialKitDomain.Default, exception);
            }
        }


        /// <summary>
        /// Requests info for age compliance.
        /// </summary>
        /// <param name="options">Options for adding available content age gate ranges. Pass null or RequestInfoForAgeComplianceOptions.Default to use default values.</param>
        /// <param name="onComplete">A callback that will be invoked when the request is completed with the result.</param>
        /// <param name="mockData">Mock data to be used for testing. Pass null to use real data.</param>
        public static void RequestInfoForAgeCompliance(RequestInfoForAgeComplianceOptions options, EventCallback<InfoForAgeCompliance> onComplete, AgeComplianceMockData mockData = null)
        {
            try
            {
                if (!s_settings.UsesAgeComplianceApi)
                {
                    DebugLogger.LogError(EssentialKitDomain.Default, "Usage of Age Compliance API is not enabled. Enable it in Essential Kit Settings -> Services -> Utilities -> Uses Age Compliance Api.");
                }

                options ??= RequestInfoForAgeComplianceOptions.Default;
                s_nativeInterface.RequestInfoForAgeCompliance(options, onComplete, mockData);
            }
            catch (Exception exception)
            {
                DebugLogger.LogException(EssentialKitDomain.Default, exception);
            }
        }
        
        #endregion
    }
}