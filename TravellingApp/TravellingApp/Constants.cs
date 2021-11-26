using System;
using System.IO;

namespace ScanApp
{
    public static class Constants
    {
        public const string DatabaseFilenameArchivos = "ArchivosSQLite.db3";
        public const string DatabaseFilenameUsuarios = "UsuariosSQLite.db3";
        public const string DatabaseFilenameImagenes = "ImagenesSQLite.db3";
        public const string DatabaseFilename = "TodoSQLite.db3";
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
        public static string DatabasePathUsuario
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilenameUsuarios);
            }
        }

        public static string DatabasePathArchivos
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilenameUsuarios);
            }
        }

        public static string DatabasePathImagenes
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilenameImagenes);
            }
        }

    }
}
