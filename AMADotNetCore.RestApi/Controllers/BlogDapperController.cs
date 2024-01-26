using AMADotNetCore.RestApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AMADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {

        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AMADotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };
        

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            string query = @"SELECT [Blog_Id]
    ,[Blog_Title]
    ,[Blog_Author]
    ,[Blog_Content]
FROM [dbo].[Tbl_Blog]";
            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] WHERE [Blog_Id] = @Blog_Id";
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return Ok("Data not found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

            int result = db.Execute(query, blog);
            var message = result > 0 ? "Saving Success." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult updateBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            #region get by id
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] WHERE [Blog_Id] = @Blog_Id";
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return Ok("Data not found.");
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

            string queryUpdate = @"UPDATE [dbo].[Tbl_Blog]
   SET [Blog_Title] = @Blog_Title
      ,[Blog_Author] = @Blog_Author
      ,[Blog_Content] = @Blog_Content
 WHERE [Blog_Id] = @Blog_Id";

            blog.Blog_Id = id;
            int result = db.Execute(queryUpdate, blog);
            var message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            #region get by id
            string query = @"SELECT [Blog_Id]
      ,[Blog_Title]
      ,[Blog_Author]
      ,[Blog_Content]
  FROM [dbo].[Tbl_Blog] WHERE [Blog_Id] = @Blog_Id";
            BlogDataModel? item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return Ok("Data not found.");
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

            if(conditions.Length == 0)
            {
                return BadRequest("Invalid request.");
            }

            conditions = conditions.Substring(0,conditions.Length - 2);

            string queryUpdate = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE [Blog_Id] = @Blog_Id";

            blog.Blog_Id = id;
            int result = db.Execute(queryUpdate, blog);
            var message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE [Blog_Id] = @Blog_Id";

            int result = db.Execute(query, new BlogDataModel { Blog_Id = id });
            string message = result > 0 ? "Delete success" : "Delete failed";
            return Ok(message);
        }
    }
}
