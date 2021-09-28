using NuGet.Common;
using SQLite;
using ScanApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScanApp.Data
{
    class UsuarioDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<UsuarioDatabase> Instance = new AsyncLazy<UsuarioDatabase>(async () =>
        {
            var instance = new UsuarioDatabase();
            CreateTableResult result = await Database.CreateTableAsync<UsuarioModel>();
            return instance;
        });

        public UsuarioDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<UsuarioModel>> GetItemsAsync()
        {
            return Database.Table<UsuarioModel>().ToListAsync();
        }

        public Task<List<UsuarioModel>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<UsuarioModel>("SELECT * FROM [UsuarioModel] WHERE [Username] = ''");
        }

        public Task<UsuarioModel> GetItemAsync(int id)
        {
            return Database.Table<UsuarioModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(UsuarioModel item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(UsuarioModel item)
        {
            return Database.DeleteAsync(item);
        }
    }

}
