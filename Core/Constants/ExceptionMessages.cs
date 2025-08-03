namespace TiktokLocalAPI.Core.Constants
{
    public static class ExceptionMessages
    {
        public static readonly string InvalidData = "Invalid provided data";
        public static readonly string InvalidUserRole = "Invalid user role";
        public static readonly string EmailAlreadyRegistered = "Email already registered";
        public static readonly string UserCreationFailed = "User creation failed";
        public static readonly string InvalidPasswordOrEmail = "Invalid password or email";
        public static readonly string AccountNew =
            "Your account has not been activated yet. Please activate it via the email.";
        public static readonly string AccountDeactivated =
            "Your account is currently deactivated. Please contact support to reactivate it.";
        public static readonly string AccountBlocked =
            "Your account is currently blocked. Please contact support to reactivate it.";
        public static readonly string SessionNotFound = "Session not found";
        public static readonly string UserProfileNotFound = "User profile not found";
        public static readonly string PasswordResetRequestNotFound =
            "Password reset request not found";
        public static readonly string OldPasswordNotMatching = "Old password does not match";
        public static readonly string MissingPermissionsToUpdate =
            "Missing permissions to update this user";
        public static readonly string CannotEditAdminRole =
            "You cannot edit the role of an admin user";
        public static readonly string MissingPermissionsEditOwnRole =
            "You cannot edit your own role";
        public static readonly string AccountStatusUpdateFailed = "Account status update failed";
        public static readonly string InvalidStatus = "Invalid user account activation status";

        public static readonly string ServiceNotFound = "Service not found";
        public static readonly string ServiceNotOwner =
            "You are not authorized to delete this service.";
        public static readonly string TitleAlreadyExists =
            "A service with a similar title already exists.";

        internal static readonly string NoFileUploaded = "No file uploaded.";
        internal static readonly string InvalidFileFormat =
            "Invalid file type. Only JPG, PNG, and GIF are allowed.";
        internal static readonly string UnknownFormat = "Image format could not be determined.";
        internal static readonly string ImageNotFound = "Image not found.";

        internal static readonly string ParentFolderNotFound = "Parent folder not found.";
        internal static readonly string FolderOwnerNotMatching =
            "The owner of the current folder does not match the owner of the parent folder.";
        internal static readonly string HierarchyDepthLimitReached =
            "The limit for this hierarchy has been reached.";
        internal static readonly string HierarchyNotFound = "Hierarchy not found.";
        internal static readonly string NotOwner = "You do not own this hierarchy.";
        internal static readonly string ParentHierarchyNotFound = "Parent hierarchy not found.";
        internal static readonly string ParentHierarchyCannotBeChildOfCurrent =
            "The parent hierarchy cannot be a child of the current hierarchy.";
        internal static readonly string HierarchyHasQuestions =
            "Hierarchy has questions associated with it.";

        internal static readonly string FileNotInUserSheet =
            "The requested file was not found in your files.";
        internal static readonly string InvalidFileIdUpdate =
            "Invalid file ID provided for update operation.";
        internal static readonly string InvalidHierarchyId = "Invalid hierarchy ID provided.";
    }
}
