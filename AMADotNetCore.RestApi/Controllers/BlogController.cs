using AMADotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext db = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = db.Blogs.ToList();
            return Ok(lst);
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public IActionResult GetBlogs(int pageNo, int pageSize)
        {
            //pageNo = 1 [1-10]
            //pageNo = 2 [11-20]
            //endRowNo = pageNo * pageSize ; 10
            //startRowNo = endRowNo - pageSize ; 6570  - 10 = 6560 + 1 = 6561
            
            var lst = db.Blogs
                 .Skip((pageNo - 1) * pageSize)
                 .Take(pageSize)
                .ToList();
            var rowCount = db.Blogs.Count();
            var pageCount = rowCount / pageSize; // 110 / 10  = 11
            if(rowCount % pageSize > 0) 
            {
                pageCount++;
            }
            return Ok( new { isEndOfPage = pageNo >= pageCount, PageCount = pageCount,PageNo = pageNo , PageSize = pageSize,Data = lst });
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("data not found .");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogDataModel blog)
        {
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            var message = result > 0 ? "Saving Success." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult updateBlog(int id, BlogDataModel blog)
        {
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("data not found .");
            }

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
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();
            var message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogDataModel blog)
        {
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("data not found .");
            }
            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if(!string.IsNullOrEmpty(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }
            var result = db.SaveChanges();
            var message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                return NotFound("data not found .");
            }
            db.Blogs.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "Delete success" : "Delete failed";
            return Ok(message);
        }
    }
}
