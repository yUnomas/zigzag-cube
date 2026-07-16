using System.Runtime.InteropServices;
using UnityEngine;

namespace VoxelBusters.EssentialKit
{
    /// <summary>
    /// Range of age in years
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AgeRange
    {
        #region Properties
        
        /// <summary>
        /// The lower bound in years. Value will be -1 if bound is not applicable
        /// </summary>
        public int LowerBound { get; private set; }

        /// <summary>
        /// The upper bound in years. Value will be -1 if bound is not applicable (for ex: adult's upper bound will be -1)
        /// </summary>
        public int UpperBound { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AgeRange"/> struct.
        /// </summary>
        /// <param name="lowerBoundInYears">The lower bound in years. Value will be -1 if bound is not applicable</param>
        /// <param name="upperBoundInYears">The upper bound in years. Value will be -1 if bound is not applicable (for ex: adult's upper bound will be -1)</param>
        public AgeRange(int lowerBoundInYears, int upperBoundInYears)
        {
            LowerBound = lowerBoundInYears;
            UpperBound = upperBoundInYears;
        }

        public override string ToString()
        {
            return string.Format("LowerBound: {0}, UpperBound: {1}", LowerBound, UpperBound);
        }
    }
}