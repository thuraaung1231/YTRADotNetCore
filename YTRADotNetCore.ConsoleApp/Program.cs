using System.Data;
using System.Data.SqlClient;
using YTRADotNetCore.ConsoleApp;
//Console.WriteLine("HelloWorld");
//Console.ReadKey();
//string connectionString="Data Source=THURA\\MSSQLSERVER2;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=sasa;";
//Console.WriteLine("Connetion String: "+connectionString);
//SqlConnection connection = new SqlConnection(connectionString);

//Console.WriteLine("Connection Opening...");
//connection.Open();
//Console.WriteLine("Connection Opened...");
//string query = @"SELECT [BlogId]
//      ,[BlogTitle]
//      ,[BlogAuthor]
//      ,[BlogContent]
//      ,[DeleteFlag]
//  FROM [dbo].[Tbl_Blog]";
//SqlCommand cmd = new SqlCommand(query,connection);
////SqlDataAdapter adapter = new SqlDataAdapter(cmd);
////DataTable dt = new DataTable();
////adapter.Fill(dt);
//SqlDataReader reader = cmd.ExecuteReader();
//while (reader.Read()) 
//{
//    Console.WriteLine(reader["BlogId"]);
//    Console.WriteLine(reader["BlogTitle"]);
//    Console.WriteLine(reader["BlogAuthor"]);
//    Console.WriteLine(reader["BlogContent"]);


//}

//Console.WriteLine("Connection closing...");
//connection.Close();
//Console.WriteLine("Connection Closed...");

////foreach (DataRow dr in dt.Rows) {
////    Console.WriteLine(dr["BlogId"]);
////    Console.WriteLine(dr["BlogTitle"]);
////    Console.WriteLine(dr["BlogAuthor"]);
////    Console.WriteLine(dr["BlogContent"]);
////} ;
//Console.WriteLine("BlogTitle");
//string title= Console.ReadLine();
//Console.WriteLine("BlogAuthor");
//string Author = Console.ReadLine();

//Console.WriteLine("BlogContent");
//string Content = Console.ReadLine();



//string connectionString2 = "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=sasa;";
//SqlConnection connection2 = new SqlConnection(connectionString2);
//connection2.Open();
//string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blog]
//           ([BlogTitle]
//           ,[BlogAuthor]
//           ,[BlogContent]
//           ,[DeleteFlag])
//     VALUES
//           (@BlogTitle
//           ,'{Author}'
//           ,'{Content}'
//           ,0 )";
//SqlCommand cmd2 = new SqlCommand(queryInsert,connection2);
//cmd2.Parameters.AddWithValue("@BlogTitle",title);
//cmd2.Parameters.AddWithValue("@BlogAuthor", Author);
//cmd2.Parameters.AddWithValue("@BlogContent", Content);
////SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
////DataTable dt = new DataTable(); 
////adapter.Fill(dt);

//int result = cmd2.ExecuteNonQuery();

//connection2.Close();
////if (result == 1)
////{
////    Console.WriteLine("saving Success");
////}
////else
////{
////    Console.WriteLine("saving fail");
////}
//Console.WriteLine(result == 1 ? "Saving Success" : "Saving FAil");

AdoDotNetExample adoDotNetExample= new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();
DapperExample dapper = new DapperExample();
//dapper.Read();
//dapper.Create("mgmg","mgmgtitle","mgmgcontext");
dapper.Edit(4);
Console.ReadKey();


