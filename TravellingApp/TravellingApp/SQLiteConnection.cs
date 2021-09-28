using SQLite;

namespace ScanApp
{
    public interface ISQLiteInterface
    {
        SQLiteConnection GetConnection(); //interface de conexion 

    }

}
