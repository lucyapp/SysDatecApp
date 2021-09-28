using NuGet.Common;
using SQLite;
using ScanApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ScanApp.Data
{
    public class CarpetaItemDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<CarpetaItemDatabase> Instance = new AsyncLazy<CarpetaItemDatabase>(async () =>
        {
            var instance = new CarpetaItemDatabase();
            CreateTableResult result = await Database.CreateTableAsync<CarpetaItem>();
            return instance;
        });

        public CarpetaItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<CarpetaItem>> GetItemsAsync()
        {
            return Database.Table<CarpetaItem>().ToListAsync();
        }

        public Task<List<CarpetaItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<CarpetaItem>("SELECT * FROM [CarpetaItem] WHERE [Done] = 0");
        }

        public Task<CarpetaItem> GetItemAsync(int id)
        {
            return Database.Table<CarpetaItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(CarpetaItem item)
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

        public Task<int> DeleteItemAsync(CarpetaItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
