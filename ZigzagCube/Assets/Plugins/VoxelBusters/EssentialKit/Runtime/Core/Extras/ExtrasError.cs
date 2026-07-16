using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EssentialKit
{
    public class ExtrasError
    {
        #region Constants

        public const string kDomain = "[Essential Kit] Utilities";

        #endregion

        #region Properties

        public static Error Unknown(string description = null) => CreateError(
            code: (int)ExtrasErrorCode.Unknown,
            description: description ?? "Unknown error."
        );

        #endregion

        #region Static methods

        private static Error CreateError(int code, string description) => new Error(
            domain: kDomain,
            code: code,
            description: description);

        #endregion
    }
}