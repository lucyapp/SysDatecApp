using NuGet.Common;
using ScanApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScanApp.Data
{
    class ImagenesDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ImagenesDatabase> Instance = new AsyncLazy<ImagenesDatabase>(async () =>
        {
            var instance = new ImagenesDatabase();
            CreateTableResult result = await Database.CreateTableAsync<ImagenesModel>();
            return instance;
        });

        public ImagenesDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<ImagenesModel>> GetItemsAsync()
        {
            return Database.Table<ImagenesModel>().ToListAsync();
        }

        public Task<List<ImagenesModel>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<ImagenesModel>("SELECT * FROM [ImagenesModel] WHERE [NombreFile] = ''");
        }

        public Task<ImagenesModel> GetItemAsync(int id)
        {
            return Database.Table<ImagenesModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ImagenesModel item)
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

        public Task<int> DeleteItemAsync(ImagenesModel item)
        {
            return Database.DeleteAsync(item);
        }
    }

}
