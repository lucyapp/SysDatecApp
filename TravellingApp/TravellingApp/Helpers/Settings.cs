using System;
using System.Diagnostics;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SysDatecScanApp.Helpers
{
    public class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string GeneralSettings
        {
            get => AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SettingsKey, value);
        }

        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            set => AppSettings.AddOrUpdateValue(UserNameKey, value);
        }

        public static string Password
        {
            get => AppSettings.GetValueOrDefault(PasswordKey, PasswordDefault);
            set => AppSettings.AddOrUpdateValue(PasswordKey, value);
        }
        public static string Email
        {
            get => AppSettings.GetValueOrDefault(EmailKey, EmailDefault);
            set => AppSettings.AddOrUpdateValue(EmailKey, value);
        }

        public static string SessionId
        {
            get => AppSettings.GetValueOrDefault(SessionIdKey, SessionIdDefault);
            set => AppSettings.AddOrUpdateValue(SessionIdKey, value);
        }
        public static string jwtToken
        {
            get => AppSettings.GetValueOrDefault(JwtTokenIdKey, jwtTokenDefault);
            set => AppSettings.AddOrUpdateValue(JwtTokenIdKey, value);
        }

        public static int IdCuenta
        {
            get => AppSettings.GetValueOrDefault(IdCuentaKey, IDCuentaDefault);
            set => AppSettings.AddOrUpdateValue(IdCuentaKey, value);
        }

        public static int IdPersonaAutenticada
        {
            get => AppSettings.GetValueOrDefault(IdPersonaAutenticadaKey, IdPersonaAutenticadaDefault);
            set => AppSettings.AddOrUpdateValue(IdPersonaAutenticadaKey, value);
        }

        public static DateTime SessionDate
        {
            get => AppSettings.GetValueOrDefault(SessionDateKey, SessionDateDefault);
            set => AppSettings.AddOrUpdateValue(SessionDateKey, value);
        }

        public static void DeleteSession()
        {
            AppSettings.Clear();
            Settings.jwtToken = string.Empty;
           
            Debug.WriteLine("Session Closed");
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = string.Empty;

        private const string PasswordKey = "password_key";
        private static readonly string PasswordDefault = string.Empty;
        private const string EmailKey = "email_key";
        private static readonly string EmailDefault = string.Empty;

        private const string SessionIdKey = "sessionid_key";
        private static readonly string SessionIdDefault = string.Empty;

        private const string JwtTokenIdKey = "jwtToken_key";
        private static readonly string jwtTokenDefault = string.Empty;

        private const string IdCuentaKey = "idcuenta_key";
        private static readonly int IDCuentaDefault = 0;

        private const string IdRolKey = "idrol_key";
        
        private const string SessionDateKey = "sessiondate_key";
        private static readonly DateTime SessionDateDefault = DateTime.Now;

        private const string IdPersonaAutenticadaKey = "IDPersonaAutenticada_key";
        private const int IdPersonaAutenticadaDefault = 0;

        #endregion
    }
}