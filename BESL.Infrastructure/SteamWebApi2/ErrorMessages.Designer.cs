﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BESL.Infrastructure.SteamWebAPI2 {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BESL.Infrastructure.SteamWebAPI2.ErrorMessages", typeof(ErrorMessages).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The provided value is a URL but not in the format from which a Steam ID can be constructed..
        /// </summary>
        public static string InvalidSteamCommunityUri {
            get {
                return ResourceManager.GetString("InvalidSteamCommunityUri", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to After multiple attempts, the Steam ID could not be constructed from the provided value..
        /// </summary>
        public static string SteamIdNotConstructed {
            get {
                return ResourceManager.GetString("SteamIdNotConstructed", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Value needs to be resolved using Steam Web API but a Web API key was not provided..
        /// </summary>
        public static string SteamWebApiKeyNotProvided {
            get {
                return ResourceManager.GetString("SteamWebApiKeyNotProvided", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The entered value for the Vanity URL could not be resolved to a Steam ID..
        /// </summary>
        public static string VanityUrlNotResolved {
            get {
                return ResourceManager.GetString("VanityUrlNotResolved", resourceCulture);
            }
        }
    }
}
