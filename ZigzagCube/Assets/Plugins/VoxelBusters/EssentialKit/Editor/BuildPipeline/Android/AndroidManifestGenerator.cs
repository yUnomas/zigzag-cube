#if UNITY_ANDROID
using System.Xml;
using UnityEditor;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.Editor.NativePlugins.Build.Android;

namespace VoxelBusters.EssentialKit.Editor.Build.Android
{
    public class AndroidManifestGenerator
    {
#region Static fields


#endregion

#region Public methods

        public static void GenerateManifest(EssentialKitSettings settings, string savePath = null)
        {
            Manifest manifest = new Manifest();
            manifest.AddAttribute("xmlns:android", "http://schemas.android.com/apk/res/android");
            manifest.AddAttribute("xmlns:tools", "http://schemas.android.com/tools");
            manifest.AddAttribute("android:versionCode", "1");
            manifest.AddAttribute("android:versionName", "1.0");

            AddQueries(manifest, settings);

            Application application = new Application();

            AddActivities(application, settings);
            AddProviders(application, settings);
            AddServices(application, settings);
            AddReceivers(application, settings);
            AddMetaData(application, settings);

            manifest.Add(application);

            AddPermissions(manifest, settings);
            AddFeatures(manifest, settings);


            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNode = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            // Append xml node
            xmlDocument.AppendChild(xmlNode);

            // Get xml hierarchy
            XmlElement element = manifest.GenerateXml(xmlDocument);
            xmlDocument.AppendChild(element);

            // Save to androidmanifest.xml
            xmlDocument.Save(savePath == null ? IOServices.CombinePath(EssentialKitPackageLayout.AndroidProjectPath, "AndroidManifest.xml") : savePath);
        }

#endregion

#region Private methods

        private static void AddQueries(Manifest manifest, EssentialKitSettings settings)
        {
        }

        private static void AddActivities(Application application, EssentialKitSettings settings)
        {
            
        }

        private static void AddProviders(Application application, EssentialKitSettings settings)
        {
            
        }

        private static void AddServices(Application application, EssentialKitSettings settings)
        {
        }

        private static void AddReceivers(Application application, EssentialKitSettings settings)
        {   
        }

        private static void AddMetaData(Application application, EssentialKitSettings settings)
        {
            
        }

        private static void AddFeatures(Manifest manifest, EssentialKitSettings settings)
        {
            
        }

        private static void AddPermissions(Manifest manifest, EssentialKitSettings settings)
        {            
        }

        private static IntentFilter CreateIntentFilterForDeepLink(bool isUniversalLinkFilter, string label, string scheme, string host, string pathPrefix = null)
        {
            IntentFilter intentFilter = new IntentFilter();
            intentFilter.AddAttribute("android:label", label);

            if(isUniversalLinkFilter)
                intentFilter.AddAttribute("android:autoVerify", "true");

            Action action = new Action();
            action.AddAttribute("android:name", "android.intent.action.VIEW");
            intentFilter.Add(action);

            Category category = new Category();
            category.AddAttribute("android:name", "android.intent.category.DEFAULT");
            intentFilter.Add(category);

            category = new Category();
            category.AddAttribute("android:name", "android.intent.category.BROWSABLE");
            intentFilter.Add(category);

            Data data = new Data();
            data.AddAttribute("android:scheme", scheme);
            if (!string.IsNullOrEmpty(host))
            {
                data.AddAttribute("android:host", host);
            }

            if (!string.IsNullOrEmpty(pathPrefix))
            {
                data.AddAttribute("android:pathPrefix", pathPrefix.StartsWith("/") ? pathPrefix :  "/" + pathPrefix);
            }

            intentFilter.Add(data);

            return intentFilter;
        }

#endregion
    }
}
#endif