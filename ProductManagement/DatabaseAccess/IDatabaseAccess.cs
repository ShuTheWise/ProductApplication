using System.Collections.Generic;

namespace ProductManagement.Database
{
    //Contract for database handling
    public interface IDatabaseAccess
    {
        T LoadSingle<T>(string sql);

        List<T> LoadData<T>(string sql);

        int SaveData<T>(string sql, T data);

        int Execute(string sql);

        string connectionString { get; set; }
    }
}
