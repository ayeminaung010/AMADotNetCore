using AMADotNetCore.MVCApp.EFCoreDbContext;
using AMADotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMADotNetCore.MVCApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly AppDbContext _context;

        public BlogAjaxController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("index")]
        public async Task<IActionResult> BlogIndex(int pageNo = 1, int pageSize = 10)
        {
            List<BlogDataModel> blogs = await _context.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.Blog_Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            int pageRowCount = await _context.Blogs.CountAsync();
            int pageCount = pageRowCount / pageSize;
            if (pageRowCount % pageSize > 0)
            {
                pageCount++;
            }
            BlogListResponseModel model = new BlogListResponseModel()
            {
                BlogList = blogs,
                PageCount = pageCount,
                PageNo = pageNo,
                PageSize = pageSize,
                PageRowCount = pageRowCount
            };
            return View("BlogIndex", model);
        }

        [ActionName("create")]
        public IActionResult BlogCreate()
        {
            string a = "Hello world.";
            ViewData["Title1"] = a;
            ViewData["Number"] = 10;  //viewData/viewBag still working..
            //ViewBag.Number = 20;   //viewData/viewBag still working..

            TempData["Title2"] = "OKay pr ...";
            TempData["Number"] = 30;
            return View("BlogCreate");
            //return Redirect("/Home");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogSave(BlogDataModel blog)
        {
            await _context.Blogs.AddAsync(blog);
            var result = await _context.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "Saving Success." : "Saving Failed." , isSuccess = result > 0 });
        }

        //generate blog data
        //public async Task<IActionResult> Generate()
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        await _context.Blogs.AddAsync(new BlogDataModel
        //        {
        //            Blog_Title = i.ToString(),
        //            Blog_Author = i.ToString(),
        //            Blog_Content = i.ToString(),
        //        });
        //        var result = await _context.SaveChangesAsync();
        //    }
        //    return Redirect("/Blog");
        //}

        [ActionName("edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No Data Found.";
                return Redirect("/Blog");
            }
            BlogDataModel? item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No Data Found.";
                return Redirect("/Blog");
            }
            return View("BlogEdit", item);
        }

        [HttpPost]
        [ActionName("update")]
        public async Task<IActionResult> BlogUpdate(BlogDataModel blog)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == blog.Blog_Id);
            if (!isExist)
            {
                return Json(new { Message = "No Data Found.", IsSuccess = false });
            }
            BlogDataModel? item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == blog.Blog_Id);
            if (item == null)
            {
                return Json(new { Message = "No Data Found.", IsSuccess = false });
            }
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;
            var result = await _context.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "Updating Success." : "Updating Failed.", IsSuccess = result > 0 });
        }

        [ActionName("delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);
            if (!isExist)
            {
                return Json(new { Message = "No Data Found.", IsSuccess = false });
            }
            BlogDataModel? item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {
                return Json(new { Message = "No Data Found.", IsSuccess = false });
            }
            _context.Blogs.Remove(item);
            var result = await _context.SaveChangesAsync();
            return Json(new { Message = result > 0 ? "Deleting Success." : "Deleting Failed.", IsSuccess = result > 0 });
        }
    }
}
