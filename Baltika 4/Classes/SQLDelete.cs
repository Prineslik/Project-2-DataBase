using System.Data;
using System.Data.OleDb;

namespace Baltika_4.Classes
{
    class SQLDelete
    {
        private static readonly string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\\\KorabliDB.mdb";
                                                           

        public static void DeleteRecord(string query, int rowIndex, OleDbDataAdapter adapter, DataSet ds)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(query + rowIndex, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
