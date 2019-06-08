namespace BESL.Common
{   
    using System.Runtime.InteropServices;

    public class DbConnectionStringHandler
    {
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
