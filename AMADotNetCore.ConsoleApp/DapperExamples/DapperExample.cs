using AMADotNetCore.ConsoleApp.Models;
using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AMADotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AMADotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };

        //private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;
        //public DapperExample() 
        //{
        //    sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        //    {
        //        DataSource = ".",
        //        InitialCatalog = "AMADotNetCore",
        //        UserID = "sa",
        //        Password = "sa@123"
        //    };
        //}

        public void Run()
        {
            //Read();
            //Edit(1);
            //Create("testing title", "testing author", "testing content");
            //Update(1, "testing title", "testing author", "testing content");
            Delete(27);
        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            string query = @"SELECT [Blog_Id]
    ,[Blog_Title]
    ,[Blog_Author]
    ,[Blog_Content]
FROM [dbo].[Tbl_Blog]";
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            foreach(BlogDataModel item in lst)
            {   
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
            
        }

        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] WHERE [Blog_Id] = @Blog_Id";
            //BlogDataModel? item = db.Query<BlogDataModel>(query,new {Blog_Id = id}).FirstOrDefault();
            BlogDataModel? item = db.Query<BlogDataModel>(query,new BlogDataModel {Blog_Id = id}).FirstOrDefault();

            if(item is null)
            {
                Console.WriteLine("Data not found.");
                return;
            }

            if(item != null)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        private void Create(string title,string author,string content)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Saving successfull." : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE [Blog_Id] = @Blog_Id";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updating successfull." : "Updating Failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE [Blog_Id] = @Blog_Id";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id
            };

            int result = db.Execute(query, new BlogDataModel { Blog_Id = id });

            string message = result > 0 ? "Deleting Successfull." : "Deleting Failed.";
            Console.WriteLine(message);
        }

    }
}
