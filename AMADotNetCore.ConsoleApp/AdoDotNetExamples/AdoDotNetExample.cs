using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMADotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {

        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(11);
            //Create("Test Title", "Test Author", "Test Content");
            //Update(1, "TEST TITLE", "TEST AUTHOR", "TEST CONTENT");
            Delete(24);
        }

        private void Read()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AMADotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Opened..");

            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog]";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);


            connection.Close();
            Console.WriteLine("Connection Closed..");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Id => " + dr["Blog_Id"]);
                Console.WriteLine($"Title => {dr["Blog_Title"]}");
                Console.WriteLine("Author => " + dr["Blog_Author"]);
                Console.WriteLine("Content => " + dr["Blog_Content"]);
            }
        }


        private void Edit(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AMADotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            Console.WriteLine("Connection Opening..");
            connection.Open();
            Console.WriteLine("Connection Opened..");

            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] WHERE [Blog_Id] = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            Console.WriteLine("Connection Closed..");

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found.");
            }

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Id => " + dr["Blog_Id"]);
                Console.WriteLine($"Title => {dr["Blog_Title"]}");
                Console.WriteLine("Author => " + dr["Blog_Author"]);
                Console.WriteLine("Content => " + dr["Blog_Content"]);
            }
        }

        private void Create(string title,string author,string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AMADotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            Console.WriteLine("Connection Opening..");
            connection.Open();
            Console.WriteLine("Connection Opened..");

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            int result = command.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection Closed..");
            string message = result > 0 ? "Saving Successfull." : "Saving Failed.";
            Console.WriteLine(message);
        }

        private void Update(int id,string title, string author, string content)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AMADotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            Console.WriteLine("Connection Opening..");
            connection.Open();
            Console.WriteLine("Connection Opened..");

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE [Blog_Id] = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);
            command.Parameters.AddWithValue("@Blog_Title", title);
            command.Parameters.AddWithValue("@Blog_Author", author);
            command.Parameters.AddWithValue("@Blog_Content", content);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            int result = command.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection Closed..");
            string message = result > 0 ? "Updating Successfull." : "Updating Failed.";
            Console.WriteLine(message);
        }


        private void Delete(int id)
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AMADotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE [Blog_Id] = @Blog_Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Id", id);

            int result = command.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Deleting Successfull." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}
