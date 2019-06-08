namespace BESL.Common
{   
    using System.Runtime.InteropServices;

    /// <summary>
    /// Class that manages connection string key names depending on the current OS.
    /// </summary>
    public class DbConnectionStringHandler
    {
        /// <summary>
        /// Runtime checks the current OS and returns a <see cref="System.String"/>
        /// with the key name of the connection string.
        /// </summary>
        /// <remarks>Values must match these in appsettings.json as they are hardcoded!</remarks>
        /// <returns>A <see cref="System.String"/> with the key value of the connection string.</returns>
        public static string GetConnectionStringNameForCurrentOS()
        {
            string connectionStringName = "DefaultConnection";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connectionStringName = "MacOSConnection";
            }         

            return connectionStringName;
        }
    }
}
