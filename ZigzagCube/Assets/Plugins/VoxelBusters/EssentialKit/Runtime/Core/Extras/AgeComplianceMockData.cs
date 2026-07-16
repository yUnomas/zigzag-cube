namespace VoxelBusters.EssentialKit
{
    /// <summary>
    /// The <see cref="AgeComplianceMockData"/> class helps mocking the age range of the user and other which are helpful in the age compliance process testing.
    /// </summary>
    public class AgeComplianceMockData
    {
        /// <summary>
        /// The age range to mock
        /// </summary>
        public AgeRange AgeRange { get; private set; }

        /// <summary>
        /// The age range declaration method to mock
        /// </summary>
        public AgeRangeDeclarationMethod AgeRangeDeclarationMethod { get; private set; }


        /// <summary>
        /// Creates a new instance of the <see cref="AgeComplianceMockData"/> class
        /// </summary>
        /// <param name="ageRange"></param>
        /// <param name="ageRangeDeclarationMethod"></param>
        public AgeComplianceMockData(AgeRange ageRange, AgeRangeDeclarationMethod ageRangeDeclarationMethod)
        {
            AgeRange = ageRange;
            AgeRangeDeclarationMethod = ageRangeDeclarationMethod;
        }
    }
}