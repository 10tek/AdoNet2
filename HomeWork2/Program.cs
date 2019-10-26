using System;
using System.Data.SqlClient;

namespace HomeWork2
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Server=DESKTOP-TALUG4B\SQLEXPRESS;Database=SalesDb;Trusted_Connection=True;";
            var sqlQuery = "Create TABLE gruppa([Id] uniqueidentifier primary key NOT NULL, [Name] nvarchar(MAX))";

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = sqlQuery;
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        Console.WriteLine(exception.Message);
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
