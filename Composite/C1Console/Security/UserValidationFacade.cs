using System;
using Composite.Core.Logging;
using Composite.C1Console.Security.Foundation.PluginFacades;
using Composite.C1Console.Security.Plugins.LoginProvider;
using System.Collections.Generic;


namespace Composite.C1Console.Security
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public static class UserValidationFacade
    {
        private static object _lock = new object();


        /// <exclude />
        public enum ValidationType 
        {
            /// <exclude />
            None = 0,

            /// <exclude />
            Form = 1,

            /// <exclude />
            Windows = 2
        };



        /// <exclude />
        public static ValidationType GetValidationType()
        {
            if (LoginProviderPluginFacade.CheckType<IFormLoginProvider>())
            {
                return ValidationType.Form;
            }
            else if (LoginProviderPluginFacade.CheckType<IWindowsLoginProvider>())
            {
                return ValidationType.Windows;
            }

            throw new InvalidOperationException(string.Format("Validation plugin '{0}' does not implement a known validation interface", LoginProviderPluginFacade.GetValidationPluginType()));
        }



        /// <exclude />
        public static IEnumerable<string> AllUsernames
        {
            get
            {
                return LoginProviderPluginFacade.AllUsernames;
            }
        }


        /// <exclude />
        public static bool CanSetUserPassword
        {
            get
            {
                return LoginProviderPluginFacade.CanSetUserPassword;
            }
        }


        /// <summary>
        /// Validates and persists a form based login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>True if the user was validated</returns>
        public static bool FormValidateUser(string userName, string password)
        {
            bool userIsValidated = LoginProviderPluginFacade.FormValidateUser(userName, password);

            if (userIsValidated == false)
            {
                lock (_lock)
                {
                    userIsValidated = LoginProviderPluginFacade.FormValidateUser(userName, password);

                    if (userIsValidated == false)
                    {
                        if (AdministratorAutoCreator.CanBeAutoCreated(userName) == true)
                        {
                            AdministratorAutoCreator.AutoCreatedAdministrator(userName, password);

                            userIsValidated = LoginProviderPluginFacade.FormValidateUser(userName, password);
                        }
                    }
                }
            }

            if (true == userIsValidated)
            {
                LoggingService.LogVerbose("UserValidation", String.Format("The user: [{0}], has been validated and accepted", userName), LoggingService.Category.Audit);
                PersistUsernameInSessionDataProvider(userName);
            }
            else
            {
                LoggingService.LogWarning("UserValidation", String.Format("Invalid login using [{0}] as user name.", userName), LoggingService.Category.Audit);
            }

            return userIsValidated;
        }



        /// <exclude />
        public static bool FormValidateUserWithoutLogin(string userName, string password)
        {
            return LoginProviderPluginFacade.FormValidateUser(userName, password);
        }



        /// <exclude />
        public static void FormSetUserPassword(string userName, string password)
        {
            LoginProviderPluginFacade.FormSetUserPassword(userName, password);
        }


        /// <summary>
        /// Validates and persists a windows based login. Actual Windows username / password validation should precede this call.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="domainName"></param>
        /// <returns>True if the user was validated</returns>
        public static bool WindowsValidateUser(string userName, string domainName)
        {
            bool userIsValidated = LoginProviderPluginFacade.WindowsValidateUser(userName, domainName);


            if (true == userIsValidated)
            {
                PersistUsernameInSessionDataProvider(userName);
            }

            return userIsValidated;
        }


        /// <summary>
        /// Flushes the username from the login session
        /// </summary>
        public static void Logout()
        {
            LoginSessionStorePluginFacade.FlushUsername();
        }



        /// <exclude />
        public static bool IsLoggedIn()
        {
            return (string.IsNullOrEmpty(LoginSessionStorePluginFacade.StoredUsername) == false);
        }



        /// <exclude />
        public static UserToken GetUserToken()
        {
            if (IsLoggedIn() == false) throw new InvalidOperationException("No user has been logged in");

            return new UserToken(LoginSessionStorePluginFacade.StoredUsername);
        }



        /// <exclude />
        public static string GetUsername()
        {
            if (IsLoggedIn() == false) throw new InvalidOperationException("No user has been logged in");

            return LoginSessionStorePluginFacade.StoredUsername;
        }



        private static void PersistUsernameInSessionDataProvider(string userName)
        {
            LoginSessionStorePluginFacade.StoreUsername(userName, false);
        }
    }
}