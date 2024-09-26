namespace TISB.DataTransfer.JsonParser.Enums {
    internal enum ResponseStatus {
        Success,
        Failed,
        Empty
    }
    public enum ErrorCodes {
        BadRequest,
        LoginOrPasswordEmpty,
        LoginOrPasswordLength,
        DuplicateLogin,
        HardDriveIdAlready,
        DriveMatches,
        DoesNotMatch,
        UserNotFound,
        IsAccessInvalid,
        IsBanned,
        LicenseInvalid,
        Unknown
    }
}
