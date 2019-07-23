﻿namespace BESL.Common
{
    public class GlobalConstants
    {
        #region Culture
        public const string DATE_FORMAT = "dd//MM//yyyy";
        #endregion

        #region Root admin credentials
        public const string ADMIN_USERNAME = "LeagueAdministrator";
        public const string ADMIN_PASSWORD = "BanHammer1000%";
        public const string ADMIN_EMAIL = "admin@besl.com";
        #endregion

        #region Exception messages
        public const string VALIDATION_EXCEPTION_BASE_MSG = "One or more validation failures have occurred.";
        public const string ENTITY_ALREADY_DELETED_MSG = "Entity is already deleted.";
        public const string ERROR_OCCURED_MSG = "An error has occured!";
        #endregion

        #region SignalR Notification messages
        public const string CREATED_SUCCESSFULLY_MSG = "has been created successfully!";
        public const string DELETED_SUCCESSFULLY_MSG = "has been deleted!";
        #endregion

        #region Steam
        public const string STEAM_PROVIDER_NAME = "Steam";
        public const string STEAM_ID_64_CLAIM_TYPE = "STEAM_ID64";
        public const string IS_VAC_BANNED_CLAIM_TYPE = "IS_VAC_BANNED";
        public const string PROFILE_AVATAR_CLAIM_TYPE = "PROFILE_AVATAR_URL";
        public const string PROFILE_AVATAR_MEDIUM_CLAIM_TYPE = "PROFILE_AVATAR_MEDIUM_URL";
        #endregion

        #region BESL
        public const string DEFAULT_AVATAR = "https://res.cloudinary.com/vasil-kotsev/image/upload/c_scale,w_184/v1563120448/Missing_avatar.svg_dykhcv.png";
        #endregion

        #region Teams
        public const int TEAM_AVATAR_HEIGHT = 184; 
        public const int TEAM_AVATAR_WIDTH = 184;
        #endregion
    }
}
