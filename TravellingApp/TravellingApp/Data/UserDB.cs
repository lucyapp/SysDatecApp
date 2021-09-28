

using SQLite;
using ScanApp.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ScanApp.Helper
{

    public class UserDB
    {
        private SQLiteConnection _SQLiteConnection;
        public UserDB()
        {
            _SQLiteConnection = DependencyService.Get<ISQLiteInterface>().GetConnection();
            _SQLiteConnection.CreateTable<UsuarioModel>();
        }

        public IEnumerable<UsuarioModel> GetUsuarios()
        {
            return (from u in _SQLiteConnection.Table<UsuarioModel>()
                    select u).ToList();
        }

        public UsuarioModel GetEspecificoUsuario(int id)
        {
            return _SQLiteConnection.Table<UsuarioModel>().FirstOrDefault(t => t.ID == id);
        }
        public void DeleteUsuario(int id)
        {
            _SQLiteConnection.Delete<UsuarioModel>(id);
        }

        public string Addusuario(UsuarioModel user)
        {
            var data = _SQLiteConnection.Table<UsuarioModel>();
            var d1 = data.Where(x => x.Email == user.Email).FirstOrDefault();
            if (d1 == null)
            {
                _SQLiteConnection.Insert(user);
                return "Datos agregados con exito";
            }
            else
                return "El  usuario ya existe";

        }


    }
}
