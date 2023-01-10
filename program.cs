using System;
using System.Data.SQLite;

class Program
{
    static void Main(string[] args)
    {
        WriteToDB("test.txt");
    }

    static void WriteToDB(string filepath)
    {
        var connectionString = "Data Source=database.db;Version=3;";
        using (var conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            var createTableCommand = "CREATE TABLE IF NOT EXISTS file_contents (contents text)";
            using (var createTable = new SQLiteCommand(createTableCommand, conn))
            {
                createTable.ExecuteNonQuery();
            }
            var fileContents = System.IO.File.ReadAllText(filepath);
            var insertCommand = "INSERT OR IGNORE INTO file_contents (contents) VALUES (@contents)";
            using (var insert = new SQLiteCommand(insertCommand, conn))
            {
                insert.Parameters.AddWithValue("@contents", fileContents);
                insert.ExecuteNonQuery();
            }
        }
    }
}
