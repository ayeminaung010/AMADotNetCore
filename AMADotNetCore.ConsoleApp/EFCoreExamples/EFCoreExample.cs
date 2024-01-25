using AMADotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AMADotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(199);
            //Create("testing title", "testing author", "testing content");
            //Update(10, "testing title", "testing author", "testing content");
            Delete(15);
        }

        public void Read()
        {
            var lst = _dbContext.Blogs.ToList();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        public void Edit(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_Id == id);

            if (item is null)
            {
                Console.WriteLine("Data not found.");
                return;
            }

            if (item != null)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        public void Create(string title,string author,string content)
        {
            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            };

            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Saving successfull." : "Saving Failed.";
            Console.WriteLine(message);
        }

        public void Update(int id,string title, string author, string content)
        {
            BlogDataModel? item = _dbContext.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("Data not found.");
                return;
            }

            item.Blog_Title = title;
            item.Blog_Author = author;
            item.Blog_Content = content;

            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updating successfull." : "Updating Failed.";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            BlogDataModel? item = _dbContext.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                Console.WriteLine("Data not found.");
                return;
            }

            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Deleting successfull." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}
