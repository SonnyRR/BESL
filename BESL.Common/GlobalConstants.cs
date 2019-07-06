﻿namespace BESL.Common
{
    public class GlobalConstants
    {
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
    }
}
