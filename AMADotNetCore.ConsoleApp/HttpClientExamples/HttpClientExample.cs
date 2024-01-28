using AMADotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMADotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        private string _blogEndpoint = "https://localhost:7163/api/blog";
        public async Task Run()
        {
            //await Read();
            //await Edit(1);
            //await Edit(101);
            //await Create("created title", "created Author", "Created Content");
             //await Update(37, "hello title 11", "testing author 2", "testing content");
            await Delete(34);
        }

        public async Task Read()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response =  await client.GetAsync(_blogEndpoint);
            if(response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonString)!;
                foreach(BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                }
            }
        }

        public async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{_blogEndpoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonString)!;
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
            else
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
        public async Task Create(string title,string author,string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            string jsonBlog= JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(_blogEndpoint, httpContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        public async Task Update(int id,string title, string author,string content)
        {
            HttpClient client = new HttpClient();

            BlogDataModel blog = new BlogDataModel
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            string jsonBlog = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
            
            HttpResponseMessage response = await client.PutAsync($"{_blogEndpoint}/{id}", httpContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        public async Task Delete(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response =  await client.DeleteAsync($"{_blogEndpoint}/{id}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
