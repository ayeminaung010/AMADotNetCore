using AMADotNetCore.ConsoleApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AMADotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IBlogApi _blogApi = RestService.For<IBlogApi>("https://localhost:7163");
        public async Task Run()
        {
            //await Read();
            //await Edit(1);
            //await Edit(1002);
            //await Create("refit title", "refit author", "refit content");
            //await Update(28, "refit title", "refit title new", "refit title new");
            await Delete(18);
        }

        public async Task Read()
        {
            var lst  = await _blogApi.GetBlogs();
            foreach (BlogDataModel item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        public async Task Edit(int id)
        {
            try
            {
                var item = await _blogApi.GetBlog(id);
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.Content!.ToString());
                Console.WriteLine(ex.ReasonPhrase!.ToString());
            }
            
        }

        public async Task Create(string title,string author,string content)
        {
            try
            {
                var blog = new BlogDataModel()
                {
                    Blog_Title = title,
                    Blog_Author = author,
                    Blog_Content = content
                };
                var response = await _blogApi.CreateBlog(blog);
                Console.WriteLine(response);
            }
            catch(Refit.ApiException ex)
            {
                Console.WriteLine(ex.Content!.ToString());
                Console.WriteLine(ex.ReasonPhrase!.ToString());
            }
        }

        public async Task Update(int id, string title, string author, string content)
        {
            try
            {
                BlogDataModel blog = new BlogDataModel()
                {
                    Blog_Title = title,
                    Blog_Author = author,
                    Blog_Content = content
                };
                var response = await _blogApi.UpdateBlog(id, blog);
                Console.WriteLine(response);
            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.Content!.ToString());
                Console.WriteLine(ex.ReasonPhrase!.ToString());
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var response = await _blogApi.DeleteBlog(id);
                Console.WriteLine(response);
            }
            catch (Refit.ApiException ex)
            {
                Console.WriteLine(ex.Content!.ToString());
                Console.WriteLine(ex.ReasonPhrase!.ToString());
            }
        }
    }
}
