﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SharedRecipe.Exceptions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class APIMSG {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal APIMSG() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SharedRecipe.Exceptions.APIMSG", typeof(APIMSG).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
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
        ///   Looks up a localized string similar to The field &apos;Email&apos; is required..
        /// </summary>
        public static string EMPTY_EMAIL {
            get {
                return ResourceManager.GetString("EMPTY_EMAIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field &apos;Name&apos; is required..
        /// </summary>
        public static string EMPTY_NAME {
            get {
                return ResourceManager.GetString("EMPTY_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field &apos;Password&apos; is required..
        /// </summary>
        public static string EMPTY_PASSWORD {
            get {
                return ResourceManager.GetString("EMPTY_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field &apos;Phone&apos; is required..
        /// </summary>
        public static string EMPTY_PHONE {
            get {
                return ResourceManager.GetString("EMPTY_PHONE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email provided is already registered..
        /// </summary>
        public static string EXISTING_EMAIL {
            get {
                return ResourceManager.GetString("EXISTING_EMAIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field &apos;Phone&apos; must be formatted as XX XXXXX-XXXX..
        /// </summary>
        public static string FORMAT_PHONE {
            get {
                return ResourceManager.GetString("FORMAT_PHONE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field &apos;Email&apos; is invalid..
        /// </summary>
        public static string INVALID_EMAIL {
            get {
                return ResourceManager.GetString("INVALID_EMAIL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password is invalid..
        /// </summary>
        public static string INVALID_PASSWORD {
            get {
                return ResourceManager.GetString("INVALID_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The field &apos;Password&apos; must be at least 6 characters long..
        /// </summary>
        public static string LENGTH_PASSWORD {
            get {
                return ResourceManager.GetString("LENGTH_PASSWORD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login successfully completed..
        /// </summary>
        public static string LOGIN_COMPLETED {
            get {
                return ResourceManager.GetString("LOGIN_COMPLETED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email or password is incorrect..
        /// </summary>
        public static string LOGIN_ERROR {
            get {
                return ResourceManager.GetString("LOGIN_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password successfully changed..
        /// </summary>
        public static string PASSWORD_CHANGED {
            get {
                return ResourceManager.GetString("PASSWORD_CHANGED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Token is expired..
        /// </summary>
        public static string TOKEN_EXPIRED {
            get {
                return ResourceManager.GetString("TOKEN_EXPIRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknow error..
        /// </summary>
        public static string UNKNOW_ERROR {
            get {
                return ResourceManager.GetString("UNKNOW_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User created successfully..
        /// </summary>
        public static string USER_CREATED {
            get {
                return ResourceManager.GetString("USER_CREATED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User is not authorized to access the method..
        /// </summary>
        public static string USER_UNAUTHORIZED {
            get {
                return ResourceManager.GetString("USER_UNAUTHORIZED", resourceCulture);
            }
        }
    }
}
