﻿using DotetTrainingB5.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using YTRADotNetCore.ConsoleApp.Models;

namespace YTRADotNetCore.ConsoleApp
{
    internal class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=HHADotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            string query = "select * from tbl_blog where DeleteFlag = 0;";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            });
            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
        }

        public void Edit(int id)
        {
            string query = "select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId;";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = id
            });

            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        public void Update(int id, string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            });
            Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
        }

        public void Delete(int id)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query,
                new BlogDapperDataModel { BlogId = id });
            Console.WriteLine(result == 1 ? "deleting Successful." : "Deleting Failed.");
        }
    }
}
