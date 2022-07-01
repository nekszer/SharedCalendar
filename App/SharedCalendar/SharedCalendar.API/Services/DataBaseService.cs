using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedCalendar.API.Services
{
    public class DataBaseService : IDataBaseService
    {
        public string ConnectionString { get; set; }

        public DataBaseService(string connection)
        {
            ConnectionString = connection;
        }

        public async Task Transaction(Func<MySqlCommand, Task<bool>> commands)
        {
            if (commands == null) return;
            using var connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();
            var transaction = await connection.BeginTransactionAsync();
            var command = connection.CreateCommand();
            var status = await commands.Invoke(command);
            if (status) await transaction.CommitAsync();
            else await transaction.RollbackAsync();
        }

        public async Task<IEnumerable<Row>> Read(string query)
        {
            using var connection = new MySqlConnection(ConnectionString);
            Console.WriteLine(query, "Connection");
            await connection.OpenAsync();
            var command = new MySqlCommand(query, connection);
            Console.WriteLine(query, "QUERY");
            using var reader = await command.ExecuteReaderAsync();

            var columns = new List<string>(reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
                columns.Add(reader.GetName(i));
            var rows = new List<Row>();
            while (await reader.ReadAsync())
            {
                var columndata = new List<Column>();
                foreach (var column in columns)
                    columndata.Add(new Column
                    {
                        Name = column,
                        Value = reader[column].ToString()
                    });
                rows.Add(new Row
                {
                    Columns = columndata
                });
            }
            return rows;
        }

        public async Task<bool> Update(string query)
        {
            using var connection = new MySqlConnection(ConnectionString);
            Console.WriteLine(query, "Connection");
            await connection.OpenAsync();
            var command = new MySqlCommand(query, connection);
            Console.WriteLine(query, "QUERY");
            var number = await command.ExecuteNonQueryAsync();
            return number > 0;
        }

        public async Task<long> Insert(string query)
        {
            using var connection = new MySqlConnection(ConnectionString);
            Console.WriteLine(query, "Connection");
            await connection.OpenAsync();
            var command = new MySqlCommand(query, connection);
            Console.WriteLine(query, "QUERY");
            var number = await command.ExecuteNonQueryAsync();
            return command.LastInsertedId;
        }

        public Task<bool> Delete(string query)
        {
            throw new NotImplementedException();
        }
    }

    public class Row
    {
        public string this[string key]
        {
            get
            {
                var column = Columns.FirstOrDefault(c => c.Name == key);
                return column.Value;
            }
        }

        public IEnumerable<Column> Columns { get; set; }

        public IEnumerable<string> Keys => Columns.Select(s => s.Name);

        public IEnumerable<string> Values => Columns.Select(s => s.Value);

        public int Count => Columns.Count();

        public bool ContainsKey(string key)
        {
            return Columns.Any(c => c.Name == key);
        }
    }

    public class Column
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }

    public class ColumnProperty : Attribute
    {
        public string Name { get; set; }
        public Type Convert { get; set; }
    }
}
