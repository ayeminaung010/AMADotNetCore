using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using AMADotNetCore.RestApi.Models;
using Dapper;

namespace AMADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetBlogs()
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
            var lst = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                BlogDataModel model = new BlogDataModel();
                model.Blog_Id = Convert.ToInt32(dr["Blog_Id"]);
                model.Blog_Title = Convert.ToString(dr["Blog_Title"]);
                model.Blog_Author = Convert.ToString(dr["Blog_Author"]);
                model.Blog_Content = Convert.ToString(dr["Blog_Content"]);
                lst.Add(model);
            }
            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
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


            if (dt.Rows.Count == 0)
            {
                return NotFound("data not found.");
            }
            BlogDataModel item = new BlogDataModel();
            foreach (DataRow dr in dt.Rows)
            {
                item.Blog_Id = Convert.ToInt32(dr["Blog_Id"]);
                item.Blog_Title = Convert.ToString(dr["Blog_Title"]);
                item.Blog_Author = Convert.ToString(dr["Blog_Author"]);
                item.Blog_Content = Convert.ToString(dr["Blog_Content"]);
            }
            return Ok(item);
        }


        [HttpPost]
        public IActionResult CreateBlogs(BlogDataModel blog)
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

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            command.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            command.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            int result = command.ExecuteNonQuery();
            connection.Close();
            var message = result > 0 ? "Saving Success." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult updateBlog(int id, BlogDataModel blog)
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

            #region get by id
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

            if (dt.Rows.Count == 0)
            {
                return NotFound("data not found.");
            }
            #endregion

            #region check required field
            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog Title is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog Author is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog Content is required.");
            }
            #endregion
            connection.Open();
            string queryUpdate = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE [Blog_Id] = @Blog_Id";
            SqlCommand commandUpdate = new SqlCommand(queryUpdate, connection);
            commandUpdate.Parameters.AddWithValue("@Blog_Id", id);
            commandUpdate.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            commandUpdate.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            commandUpdate.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            SqlDataAdapter sqlDataAdapterUpdate = new SqlDataAdapter(commandUpdate);

            DataTable dtUpdate = new DataTable();
            sqlDataAdapterUpdate.Fill(dtUpdate);
            int result = commandUpdate.ExecuteNonQuery();

            connection.Close();
            var message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
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

            #region get by id
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

            if (dt.Rows.Count == 0)
            {
                return NotFound("data not found.");
            }
            #endregion

            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += @" [Blog_Title] = @Blog_Title, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += @" [Blog_Author] = @Blog_Author, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += @" [Blog_Content] = @Blog_Content, ";
            }

            
            if (conditions.Length == 0)
            {
                return BadRequest("Invalid request.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            connection.Open();
            string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE [Blog_Id] = @Blog_Id";

            SqlCommand commandUpdate = new SqlCommand(queryUpdate, connection);
            commandUpdate.Parameters.AddWithValue("@Blog_Id", id);
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                commandUpdate.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                commandUpdate.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                commandUpdate.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            }
            
            SqlDataAdapter sqlDataAdapterUpdate = new SqlDataAdapter(commandUpdate);

            DataTable dtUpdate = new DataTable();
            sqlDataAdapterUpdate.Fill(dtUpdate);
            int result = commandUpdate.ExecuteNonQuery();

            connection.Close();
            var message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
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

            #region get by id
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

            if (dt.Rows.Count == 0)
            {
                return NotFound("data not found.");
            }
            #endregion
            connection.Open();
            string queryDelete = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE [Blog_Id] = @Blog_Id";

            SqlCommand commandDelete = new SqlCommand(queryDelete, connection);
            commandDelete.Parameters.AddWithValue("@Blog_Id", id);

            int result = commandDelete.ExecuteNonQuery();

            connection.Close();
            string message = result > 0 ? "Delete success" : "Delete failed";
            return Ok(message);
        }
    }
}
