﻿namespace BESL.Common
{
    public class GlobalConstants
    {
        #region Culture
        public const string DATE_FORMAT = @"dd/MM/yyyy";
        public const string DATEPICKER_DATE_FORMAT = "yyyy-MM-dd";
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
        public const string PLAYER_CAN_ONLY_DELETE_HIS_OWN_NOTIFICATIONS_MSG = "You can delete only your own notifications!";
        #endregion

        #region Notifications
        public const string CREATED_SUCCESSFULLY_MSG = "has been created successfully!";
        public const string DELETED_SUCCESSFULLY_MSG = "has been deleted successfully!";
        public const string MODIFIED_SUCCESSFULLY_MSG = "has been modified successfully!";
        public const string NOTIFICATION_ENTITY_HEADER_TEMPLATE_MSG = "{0} [Id:{1}]";
        public const double REDIS_NOTIFICATION_EXPIRATION_MINUTES = 2;
        #endregion

        #region Steam
        public const string STEAM_PROVIDER_NAME = "Steam";
        public const string STEAM_ID_64_CLAIM_TYPE = "STEAM_ID64";
        public const string IS_VAC_BANNED_CLAIM_TYPE = "IS_VAC_BANNED";
        public const string PROFILE_AVATAR_CLAIM_TYPE = "PROFILE_AVATAR_URL";
        public const string PROFILE_AVATAR_MEDIUM_CLAIM_TYPE = "PROFILE_AVATAR_MEDIUM_URL";
        #endregion

        #region BESL
        public const string DEFAULT_AVATAR = "https://res.cloudinary.com/vasil-kotsev/image/upload/c_scale,w_184/v1563120448/BESL/Missing_avatar.svg_dykhcv.png";
        public const string DEFAULT_AVATAR_MEDIUM = "https://res.cloudinary.com/vasil-kotsev/image/upload/c_scale,w_64/v1563120448/BESL/Missing_avatar.svg_dykhcv.png";
        #endregion

        #region Teams
        public const int TEAM_AVATAR_HEIGHT = 184; 
        public const int TEAM_AVATAR_WIDTH = 184;
        #endregion

        #region Games
        public const int GAME_IMAGE_HEIGHT = 215;
        public const int GAME_IMAGE_WIDTH = 460;
        #endregion

        #region Tournaments
        public const int TOURNAMENT_FORMAT_PLAYERS_MULTIPLIER = 2;
        #endregion

        #region Tournaments
        public const int TOURNAMENT_IMAGE_HEIGHT = 215;
        public const int TOURNAMENT_IMAGE_WIDTH = 460;
        public const int OPEN_TABLE_MAX_TEAMS = 50;
        public const int MID_TABLE_MAX_TEAMS = 50;
        public const int PREM_TABLE_MAX_TEAMS = 20;
        public const string OPEN_TABLE_NAME = "Open";
        public const string MID_TABLE_NAME = "Mid";
        public const string PREM_TABLE_NAME = "Premiership";
        #endregion
    }
}
