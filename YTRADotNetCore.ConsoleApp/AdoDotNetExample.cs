using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTRADotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString= "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=sasa;";


        public void Read()
        {
            Console.WriteLine("Connetion String: " + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine("Connection Opening...");
            connection.Open();
            Console.WriteLine("Connection Opened...");
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog]";
            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);


            }

            Console.WriteLine("Connection closing...");
            connection.Close();
            Console.WriteLine("Connection Closed...");

            //foreach (DataRow dr in dt.Rows) {
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //} ;

        }
        public void Create()
        {
            Console.WriteLine("BlogTitle");
            string title = Console.ReadLine();
            Console.WriteLine("BlogAuthor");
            string Author = Console.ReadLine();

            Console.WriteLine("BlogContent");
            string Content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,'{Author}'
           ,'{Content}'
           ,0 )";
            SqlCommand cmd2 = new SqlCommand(queryInsert, connection);
            cmd2.Parameters.AddWithValue("@BlogTitle", title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", Author);
            cmd2.Parameters.AddWithValue("@BlogContent", Content);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            //DataTable dt = new DataTable(); 
            //adapter.Fill(dt);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();
            //if (result == 1)
            //{
            //    Console.WriteLine("saving Success");
            //}
            //else
            //{
            //    Console.WriteLine("saving fail");
            //}
            Console.WriteLine(result == 1 ? "Saving Success" : "Saving FAil");

            Console.ReadKey();

        }
        public void Edit()
        {
            Console.Write("Blog Id : ");
            string id = Console.ReadLine();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog] where [BlogId]=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId",id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); 
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
            { 
            Console.WriteLine("NO Data Found");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);

        }
        public void Update()
        {
            Console.WriteLine("Blog Id");
            string id = Console.ReadLine(); 

            Console.WriteLine("BlogTitle");
            string title = Console.ReadLine();
            Console.WriteLine("BlogAuthor");
            string Author = Console.ReadLine();

            Console.WriteLine("BlogContent");
            string Content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string queryInsert = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] =@BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId=@BlogId";
            SqlCommand cmd2 = new SqlCommand(queryInsert, connection);
            cmd2.Parameters.AddWithValue("@BlogId", id);

            cmd2.Parameters.AddWithValue("@BlogTitle", title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", Author);
            cmd2.Parameters.AddWithValue("@BlogContent", Content);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            //DataTable dt = new DataTable(); 
            //adapter.Fill(dt);

            int result = cmd2.ExecuteNonQuery();

            connection.Close();
            //if (result == 1)
            //{
            //    Console.WriteLine("saving Success");
            //}
            //else
            //{
            //    Console.WriteLine("saving fail");
            //}
            Console.WriteLine(result == 1 ? "Saving Success" : "Saving FAil");

        }
        public void Delete()
        {
            Console.WriteLine("Please Enter Blog Id");
            string blogId = Console.ReadLine();
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                                  WHERE BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            int result = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            Console.WriteLine(result > 0 ? "Successfully Deleted" : "Failed to Delete");
            Console.ReadKey();
        }
    }
}
