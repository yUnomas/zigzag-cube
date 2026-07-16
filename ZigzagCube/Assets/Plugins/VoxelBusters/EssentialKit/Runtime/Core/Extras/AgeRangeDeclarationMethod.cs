namespace VoxelBusters.EssentialKit
{
    /// <summary>
    /// The age range declaration method
    /// </summary>
    public enum AgeRangeDeclarationMethod
    {
        /// <summary>
        /// The age range is not declared. AgeRange's lower and upper bound values are set to 0
        /// </summary>
        NotDeclared = 0,

        /// <summary>
        /// The age range is declared by the user
        /// </summary>
        DeclaredBySelf,

        /// <summary>
        /// The age range is declared with a valid payment method ex: credit card
        /// </summary>
        DeclaredWithPayment,

        /// <summary>
        /// The age range is declared with a valid id ex: passport or government id
        /// </summary>
        DeclaredWithValidId,

        /// <summary>
        /// The age range is declared with any other method
        /// </summary>
        DeclaredWithOther,

        /// <summary>
        /// The age range is declared by the guardian
        /// </summary>
        DeclaredByGuardian,

        /// <summary>
        /// The age range is declared by the guardian with a valid payment method
        /// </summary>
        DeclaredByGuardianWithPayment,

        /// <summary>
        /// The age range is declared by the guardian with a valid id
        /// </summary>
        DeclaredByGuardianWithValidId,

        /// <summary>
        /// The age range is declared by the guardian with any other method
        /// </summary>
        DeclaredByGuardianWithOther,

        /// <summary>
        /// The age range declaration method is unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// The age range declaration is not applicable
        /// </summary>
        NotApplicable
    }
}