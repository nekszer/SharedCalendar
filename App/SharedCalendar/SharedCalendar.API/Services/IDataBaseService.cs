using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedCalendar.API.Services
{
    public interface IDataBaseService
    {
        Task<IEnumerable<Row>> Read(string query);
        Task<bool> Update(string query);
        Task<long> Insert(string query);
        Task<bool> Delete(string query);
        Task Transaction(Func<MySqlCommand, Task<bool>> commands);
    }
}