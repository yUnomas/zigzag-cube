#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EssentialKit.Common.Android;

namespace VoxelBusters.EssentialKit.ExtrasCore.Android
{
    public class NativeAgeRange : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion
        #region Constructor

        // Default constructor
        public NativeAgeRange(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }
        public NativeAgeRange(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }
        public NativeAgeRange(int lowerBound, int upperBound) : base(Native.kClassName ,(object)lowerBound, (object)upperBound)
        {
        }

#if NATIVE_PLUGINS_DEBUG_ENABLED
        ~NativeAgeRange()
        {
            DebugLogger.Log("Disposing NativeAgeRange");
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

        public int GetLowerBound()
        {
            return Call<int>(Native.Method.kGetLowerBound);
        }
        public int GetUpperBound()
        {
            return Call<int>(Native.Method.kGetUpperBound);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.essentialkit.extras.AgeRange";

            internal class Method
            {
                internal const string kGetUpperBound = "getUpperBound";
                internal const string kGetLowerBound = "getLowerBound";
            }

        }
    }
}
#endif