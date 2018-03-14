using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AdoNetLesson02
{
    class Program
    {
        static SqlConnection sqlConnection = null;
        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "CR4-00";
            builder.InitialCatalog = "Library";
            builder.IntegratedSecurity = true;
            sqlConnection = new SqlConnection(builder.ConnectionString);
            ReadData();
            Console.ReadKey();
        }

        private static void ReadData()
        {
            SqlDataReader reader = null;
            try
            {
                sqlConnection.Open();
                string sql = "select * from authors;select * from books";
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                reader = sqlCommand.ExecuteReader();

                int line = 0;

                do
                {
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader.GetName(i).ToString() + "\t");
                            }
                            Console.WriteLine();
                        }
                        line++;

                        Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2]);

                    }
                } while (reader.NextResult());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(sqlConnection!=null)
                    sqlConnection.Close();
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
