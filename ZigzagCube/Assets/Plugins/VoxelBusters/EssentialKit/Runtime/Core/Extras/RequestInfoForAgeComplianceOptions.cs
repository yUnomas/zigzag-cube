using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EssentialKit
{
    /// <summary>
    /// The <see cref="RequestInfoForAgeComplianceOptions"/> can be used to provide additional info to the <see cref="Utilities.RequestInfoForAgeCompliance(RequestInfoForAgeComplianceOptions, EventCallback{InfoForAgeCompliance})"/>
    /// </summary>
    public class RequestInfoForAgeComplianceOptions
    {
        public static RequestInfoForAgeComplianceOptions Default { get; } = new RequestInfoForAgeComplianceOptions()
        {
            AvailableContentAgeGates = new AgeRange[1] { new AgeRange(0, 100) }
        };

        /// <summary>
        /// The available age gates for content.
        /// </summary>
        public AgeRange[] AvailableContentAgeGates { get; private set; }

        private RequestInfoForAgeComplianceOptions() {}

        /// <summary>
        /// The builder class for <see cref="RequestInfoForAgeComplianceOptions"/>
        /// </summary>
        public class Builder
        {
            private List<AgeRange> m_contentAgeGateRanges = new List<AgeRange>();
            public Builder()
            {
            }

            /// <summary>
            /// Adds a content age gate range. 
            /// Provide this info if you have different content based on age. By default, 0-100 content age gate range is considered.
            /// </summary>
            /// <param name="lowerBoundInYears">The lower bound in years</param>
            /// <param name="upperBoundInYears">The upper bound in years</param>
            /// <returns></returns>
            /// <exception cref="ArgumentException"></exception>
            public Builder AddContentAgeGateRange(int lowerBoundInYears, int upperBoundInYears)
            {
                if (lowerBoundInYears < 0 || upperBoundInYears < 0 || (upperBoundInYears - lowerBoundInYears <= 1))
                {
                    throw new ArgumentException("Range must have atleast duration of 2 years.");
                }

                m_contentAgeGateRanges.Add(new AgeRange(lowerBoundInYears, upperBoundInYears));
                return this;
            }


            /// <summary>
            /// Builds the <see cref="RequestInfoForAgeComplianceOptions"/>
            /// </summary>
            /// <returns></returns>
            public RequestInfoForAgeComplianceOptions Build()
            {
                if (m_contentAgeGateRanges.Count == 0)
                {
                    return Default;
                }

                var options = new RequestInfoForAgeComplianceOptions();
                options.AvailableContentAgeGates = m_contentAgeGateRanges.ToArray();
                return options;
            }
        }
    }
}